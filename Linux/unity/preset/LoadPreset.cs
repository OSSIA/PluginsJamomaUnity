using UnityEngine;
using System.Collections;
using Namespace;
using Ossia;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

unsafe public class LoadPreset : MonoBehaviour {
	
	public string jsonname = "Assets/preset.json";

	public delegate void debug_log_delegate(string str);
	static void DebugLogCallback(string str)
	{
		Debug.Log("BLUEYETI : " + str);
	}
	// Use this for initialization
	void Start () {

		// Setup debug log delegate
		debug_log_delegate callback_delegate = new debug_log_delegate (DebugLogCallback);

		// Convert callback_delegate into a function pointer that can be
		// used in unmanaged code.
		IntPtr intptr_delegate = 
			Marshal.GetFunctionPointerForDelegate (callback_delegate);

		// Call the API passing along the function pointer.
		BlueYetiAPI.blueyeti_set_debug_logger (intptr_delegate);


		 
		string jsontext = System.IO.File.ReadAllText (jsonname);
		Debug.Log (jsontext);

		Preset p = new Preset (jsontext);

		try {
			Debug.Log(p.ToString());
		}
		catch (Exception e) {
			Debug.Log (e.Message);
		}

		try {
			Debug.Log(p.Size());
		}
		catch (Exception e) {
			Debug.Log (e.Message);
		}

		try {
			Debug.Log(p.WriteJson());
		}
		catch (Exception e) {
			Debug.Log (e.Message);
		}
		//Debug.Log ("Loaded preset \"" + p.ToString () + "\" (" + p.Size() + " elements)");


		try {

			GameObject controller = GameObject.Find ("OssiaController");
			if (controller == null) {
				throw new Exception("Controller not found");
			}

			OssiaDevices dev = controller.GetComponent<OssiaDevices> ();
			if (dev == null) {
				throw new Exception("Device is null");
			}
				
			Ossia.Device local_device = dev.GetDevice();
			if (local_device == null) {
				throw new Exception("LocalDevice is null");
			}

			IntPtr dev_ptr = local_device.GetDevice();
			if(dev_ptr == IntPtr.Zero) {
				throw new Exception("DevPtr is null");
			}
				
			//IntPtr dummyProtocol = Ossia.Network.ossia_protocol_local_create ();
			//IntPtr dummyDevice = Ossia.Network.ossia_device_create (dummyProtocol, "dummy");

			p.ApplyToDevice(local_device, true);
			//IntPtr res;
			//BlueYetiAPI.blueyeti_devices_to_string(dev.GetDevice().GetDevice(), &res);
			//Debug.Log(Marshal.PtrToStringAuto(res));
		}
		catch (Exception e) {
			Debug.Log (e.Message);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
