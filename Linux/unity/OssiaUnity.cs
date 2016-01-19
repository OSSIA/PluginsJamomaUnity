﻿using UnityEngine;
using System.Runtime;
using System.Runtime.InteropServices;
using System;

namespace Ossia
{
	public enum ossia_type
	{
		IMPULSE,
		BOOL,
		INT,
		FLOAT,
		CHAR,
		STRING,
		TUPLE,
		GENERIC,
		DESTINATION,
		BEHAVIOR
	}

	public enum ossia_access_mode
	{
		GET,
		SET,
		BI
	}

	public enum ossia_bounding_mode
	{
		FREE,
		CLIP,
		WRAP,
		FOLD
	}

	internal class Network
	{
		[DllImport ("ossia")]
		public static extern IntPtr ossia_protocol_local_create ();

		[DllImport ("ossia")]
		public static extern IntPtr ossia_protocol_osc_create (
			string ip, 
			int in_port, 
			int out_port);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_protocol_minuit_create (
			string ip, 
			int in_port, 
			int out_port);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_device_create (
			IntPtr protocol,
			string name);

		[DllImport ("ossia")]
		public static extern void ossia_device_free (
			IntPtr device);

		[DllImport ("ossia")]
		public static extern bool ossia_device_update_namespace (
			IntPtr device);


		[DllImport ("ossia")]
		public static extern IntPtr ossia_device_add_child (
			IntPtr device,
			string name);

		[DllImport ("ossia")]
		public static extern void ossia_device_remove_child (
			IntPtr device,
			IntPtr node);

		[DllImport ("ossia")]
		public static extern int ossia_device_child_size (
			IntPtr device);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_device_get_child (
			IntPtr device,
			int child_n);

		//// Node ////

		[DllImport ("ossia")]
		public static extern IntPtr ossia_node_add_child (
			IntPtr node,
			string name);

		[DllImport ("ossia")]
		public static extern void ossia_node_remove_child (
			IntPtr node,
			IntPtr child);


		[DllImport ("ossia")]
		public static extern void ossia_node_free (
			IntPtr node);


		[DllImport ("ossia")]
		public static extern int ossia_node_child_size (
			IntPtr node);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_node_get_child (
			IntPtr node,
			int child_n);


		[DllImport ("ossia")]
		public static extern IntPtr ossia_node_create_address (
			IntPtr node,
			ossia_type type);

		[DllImport ("ossia")]
		public static extern void ossia_node_remove_address (
			IntPtr node,
			IntPtr address);


		//// Address ////

		[DllImport ("ossia")]
		public static extern void ossia_address_set_access_mode (
			IntPtr address,
			ossia_access_mode am);

		[DllImport ("ossia")]
		public static extern ossia_access_mode ossia_address_get_access_mode (
			IntPtr address);


		[DllImport ("ossia")]
		public static extern void ossia_address_set_bounding_mode (
			IntPtr address,
			ossia_bounding_mode bm);

		[DllImport ("ossia")]
		public static extern ossia_bounding_mode ossia_address_get_bounding_mode (
			IntPtr address);


		[DllImport ("ossia")]
		public static extern void ossia_address_set_domain (
			IntPtr address,
			IntPtr domain);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_address_get_domain (
			IntPtr address);


		[DllImport ("ossia")]
		public static extern void ossia_address_set_value (
			IntPtr address,
			IntPtr value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_address_get_value (
			IntPtr address);



		[DllImport ("ossia")]
		public static extern void ossia_address_push_value (
			IntPtr address,
			IntPtr value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_address_pull_value (
			IntPtr address);


		[DllImport ("ossia")]
		public static extern IntPtr ossia_address_add_callback (
			IntPtr address,
			IntPtr callback);

		[DllImport ("ossia")]
		public static extern void ossia_address_remove_callback (
			IntPtr address,
			IntPtr index);



		//// Domain ////

		[DllImport ("ossia")]
		public static extern IntPtr ossia_domain_get_min (
			IntPtr domain);

		[DllImport ("ossia")]
		public static extern void ossia_domain_set_min (
			IntPtr domain,
			IntPtr value);


		[DllImport ("ossia")]
		public static extern IntPtr ossia_domain_get_max (
			IntPtr domain);

		[DllImport ("ossia")]
		public static extern void ossia_domain_set_max (
			IntPtr domain,
			IntPtr value);


		[DllImport ("ossia")]
		public static extern void ossia_domain_free (
			IntPtr address);

		//// Value ////

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_impulse ();

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_int (int value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_float (float value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_bool (bool value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_char (char value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_string (string value);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_value_create_tuple (IntPtr[] values, int size);


		[DllImport ("ossia")]
		public static extern void ossia_value_free (IntPtr value);


		[DllImport ("ossia")]
		public static extern ossia_type ossia_value_get_type (IntPtr type);

		[DllImport ("ossia")]
		public static extern int ossia_value_to_int (IntPtr val);

		[DllImport ("ossia")]
		public static extern float ossia_value_to_float (IntPtr val);

		[DllImport ("ossia")]
		public static extern bool ossia_value_to_bool (IntPtr val);

		[DllImport ("ossia")]
		public static extern char ossia_value_to_char (IntPtr val);

		[DllImport ("ossia")]
		public static extern string ossia_value_to_string (IntPtr val);

		[DllImport ("ossia")]
		public static extern void ossia_value_free_string (string str);

		[DllImport ("ossia")]
		public static extern void ossia_value_to_tuple (
			IntPtr val_in, 
			IntPtr val_out, 
			IntPtr size);

		[DllImport ("ossia")]
		public static extern void ossia_set_debug_logger( IntPtr fp );
	}

	public class Protocol
	{
		internal IntPtr ossia_protocol;
		protected Protocol(IntPtr impl)
		{
			ossia_protocol = impl;			
		}
	}

	public class Local : Protocol
	{
		public Local() : 
		base(Network.ossia_protocol_local_create())
		{
		}
	}

	public class Minuit : Protocol
	{
		public Minuit(string ip, int in_port, int out_port) : 
		base(Network.ossia_protocol_minuit_create(ip, in_port, out_port))
		{
		}
	}

	public class OSC : Protocol
	{
		public OSC(string ip, int in_port, int out_port) : 
		base(Network.ossia_protocol_osc_create(ip, in_port, out_port))
		{
		}
	}

	public class ValueFactory
	{
		static public Value createString(string v)
		{
			return new Value(Network.ossia_value_create_string(v));
		}
		static public Value createInt(int v)
		{
			return new Value(Network.ossia_value_create_int(v));
		}
		static public Value createFloat(float v)
		{
			return new Value(Network.ossia_value_create_float(v));
		}
		static public Value createBool(bool v)
		{
			return new Value(Network.ossia_value_create_bool(v));
		}
		static public Value createChar(char v)
		{
			return new Value(Network.ossia_value_create_char(v));
		}
	}
	public class Value
	{
		internal IntPtr ossia_value;

		internal protected Value(IntPtr v)
		{
			ossia_value = v;
		}

		public void Free()
		{
			Network.ossia_value_free (ossia_value);
		}
	}


	public class Device
	{
		internal IntPtr ossia_device;

		public Device(Protocol proto, string name)
		{
			ossia_device = Network.ossia_device_create(proto.ossia_protocol, name);
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
	}

	public class Address
	{
		internal IntPtr ossia_address;
		public Address(IntPtr address)
		{
			ossia_address = address;
		}

		public void SetAccessMode(ossia_access_mode m)
		{
			Network.ossia_address_set_access_mode (ossia_address, m);
		}

		public ossia_access_mode GetAccessMode()
		{
			return Network.ossia_address_get_access_mode (ossia_address);
		}

		public void SetBoundingMode(ossia_bounding_mode m)
		{
			Network.ossia_address_set_bounding_mode (ossia_address, m);
		}

		public ossia_bounding_mode GetBoundingMode()
		{
			return Network.ossia_address_get_bounding_mode (ossia_address);
		}

		public void SetValue(Value val)
		{
			Network.ossia_address_set_value (ossia_address, val.ossia_value);
		}

		public Value GetValue()
		{
			return new Value (Network.ossia_address_get_value (ossia_address));
		}

		public void PushValue(Value val)
		{
			Network.ossia_address_push_value (ossia_address, val.ossia_value);
		}

		public Value PullValue()
		{
			return new Value (Network.ossia_address_pull_value (ossia_address));
		}

		/* TODO

		[DllImport ("ossia")]
		public static extern void ossia_address_set_domain (
			IntPtr address,
			IntPtr domain);

		[DllImport ("ossia")]
		public static extern IntPtr ossia_address_get_domain (
			IntPtr address);


		[DllImport ("ossia")]
		public static extern IntPtr ossia_address_add_callback (
			IntPtr address,
			IntPtr callback);

		[DllImport ("ossia")]
		public static extern void ossia_address_remove_callback (
			IntPtr address,
			IntPtr index);

        */
	}

	public class Node
	{
		internal IntPtr ossia_node;
		Address ossia_address;

		public Node(IntPtr n)
		{
			ossia_node = n;
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
			Network.ossia_node_free (ossia_node);
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
			
	}

}