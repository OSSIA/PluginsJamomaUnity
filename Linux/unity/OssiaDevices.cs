using UnityEngine;
using UnityEditor;
using System.Runtime;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using Ossia;


public class OssiaDevices : MonoBehaviour {
	static bool set = false;
	static Ossia.Local local_protocol;
	static Ossia.Device local_device;

	static Ossia.Minuit minuit_protocol;
	static Ossia.Device minuit_device;

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

			local_protocol = new Ossia.Local();
			local_device = new Ossia.Device(local_protocol, "myGame");

			scene_node = local_device.AddChild("scene");


			minuit_protocol = new Ossia.Minuit(
				"127.0.0.1", 
				13579, 
				9998);
			minuit_device = new Ossia.Device(
				minuit_protocol,
				"i-score");
		}
	}

	public Ossia.Node SceneNode()
	{
		return scene_node; 
	}


	void OnApplicationQuit() {
		minuit_device.Free ();
		local_device.Free ();
	}


}