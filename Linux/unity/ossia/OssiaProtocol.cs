﻿using UnityEngine;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

namespace Ossia
{
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
}