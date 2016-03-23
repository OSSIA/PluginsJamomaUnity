using UnityEngine;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

namespace Ossia {
	public class Node : IDisposable
	{
		internal IntPtr ossia_node = IntPtr.Zero;
		Address ossia_address;
		bool disposed = false;

		public Node(IntPtr n)
		{
			ossia_node = n;
		}

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
				Free();
			}

			disposed = true;
		}

		public string GetName()
		{
			IntPtr nameptr = Network.ossia_node_get_name (ossia_node);
			if (nameptr == IntPtr.Zero)
				return "ENONAME";
			string name = Marshal.PtrToStringAnsi (nameptr);
			Network.ossia_string_free(nameptr);
			return name;
		}


		public Node AddChild (string name)
		{
			return new Node(Network.ossia_node_add_child (ossia_node, name));
		}

		public void RemoveChild(Node child)
		{
			Network.ossia_node_remove_child (ossia_node, child.ossia_node);
			child.Free ();
		}

		public void Free()
		{
			//Network.ossia_node_free (ossia_node);
		}

		public int ChildSize()
		{
			return Network.ossia_node_child_size (ossia_node);
		}

		public Node GetChild(int child)
		{
			return new Node(Network.ossia_node_get_child (ossia_node, child));
		}

		public Address CreateAddress(ossia_type type)
		{
			ossia_address = new Address (Network.ossia_node_create_address (ossia_node, type));
			return ossia_address;
		}

		public void RemoveAddress()
		{
			Network.ossia_node_remove_address (ossia_node, ossia_address.ossia_address);
		}

		public IntPtr GetNode() {
			return ossia_node;
		}

		public Ossia.Address GetAddress() {
			return ossia_address;
		}

	}
}