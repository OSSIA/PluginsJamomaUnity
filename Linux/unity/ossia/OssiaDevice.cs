using UnityEngine;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

namespace Ossia {
	public class Device
	{
		internal IntPtr ossia_device = IntPtr.Zero;
		bool disposed = false;

		public Device(Protocol proto, string name)
		{
			ossia_device = Network.ossia_device_create(proto.ossia_protocol, name);
			Debug.Log ("Created device address : " + ossia_device);
		}
		/*
			public void Dispose()
			{ 
				Dispose(true);
				GC.SuppressFinalize(this);           
			}

			protected virtual void Dispose(bool disposing)
			{
				if (disposed)
					return; 

				if (disposing) {
					Free ();
				}

				disposed = true;
			}
			*/

		public string GetName()
		{
			IntPtr nameptr = Network.ossia_device_get_name (ossia_device);
			if (nameptr == IntPtr.Zero)
				return "ENONAME";
			string name = Marshal.PtrToStringAnsi (nameptr);
			Network.ossia_string_free(nameptr);
			return name;
		}

		public Node AddChild (string name)
		{
			return new Node(Network.ossia_device_add_child (ossia_device, name));
		}

		public void RemoveChild(Node child)
		{
			Network.ossia_device_remove_child (ossia_device, child.ossia_node);
			child.Free ();
		}

		public void Free()
		{
			Network.ossia_device_free (ossia_device);
		}

		public int ChildSize()
		{
			return Network.ossia_device_child_size (ossia_device);
		}

		public Node GetChild(int child)
		{
			return new Node(Network.ossia_device_get_child (ossia_device, child));
		}

		public IntPtr GetDevice() {
			return ossia_device;
		}
	}
}