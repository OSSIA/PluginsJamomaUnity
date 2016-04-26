using UnityEngine;
using System.Runtime;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using Ossia;


public class OssiaDevices : MonoBehaviour {
	static bool set = false;
	static Ossia.Local local_protocol = null;

	static Ossia.Device local_device = null;

	static Ossia.Minuit minuit_protocol = null;
	static Ossia.Device minuit_device = null;

	static Ossia.Node scene_node;
	Ossia.Network main;


	public delegate void debug_log_delegate(string str);
	static void DebugLogCallback(string str)
	{
		Debug.Log("OSSIA : " + str);
	}

	void Awake ()
	{
		if (!set) {
				set = true;
				debug_log_delegate callback_delegate = new debug_log_delegate (DebugLogCallback);

				// Convert callback_delegate into a function pointer that can be
				// used in unmanaged code.
				IntPtr intptr_delegate = 
					Marshal.GetFunctionPointerForDelegate (callback_delegate);

				// Call the API passing along the function pointer.
				Ossia.Network.ossia_set_debug_logger (intptr_delegate);

				local_protocol = new Ossia.Local ();
				local_device = new Ossia.Device (local_protocol, "newDevice");

			    while (!(local_device.ChildSize () == 0)) {
			    	local_device.RemoveChild (local_device.GetChild (local_device.ChildSize () - 1));
			    }

			    Debug.Log (local_device.GetName ());
				scene_node = local_device.AddChild ("scene");


				minuit_protocol = new Ossia.Minuit (
					"127.0.0.1", 
					13579, 
					9998);
				minuit_device = new Ossia.Device (
					minuit_protocol,
					"i-score");
				Debug.Log ("Created ossia devices");
		}
	}

	public Ossia.Node SceneNode()
	{
		return scene_node; 
	}


	void OnApplicationQuit() {

		while (!(minuit_device.ChildSize () == 0)) {
			minuit_device.RemoveChild (minuit_device.GetChild (minuit_device.ChildSize () - 1));
		}
		minuit_device.Free ();
		while (!(local_device.ChildSize () == 0)) {
			local_device.RemoveChild (local_device.GetChild (local_device.ChildSize () - 1));
		}
		local_device.Free ();

		Debug.Log ("Freed ossia devices");
	}

	public Ossia.Device GetDevice() {
		return local_device;
	}

	public Ossia.Protocol GetProtocol() {
		return local_protocol;
	}

}