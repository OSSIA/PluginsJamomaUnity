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
	public Preset p;

	void createCube(Ossia.Node node) {
		Debug.Log ("Creating " + node.GetName ());
		GameObject createdgo = GameObject.CreatePrimitive (PrimitiveType.Cube);
		createdgo.name = node.GetName ();
		Rigidbody rb = createdgo.GetComponent<Rigidbody> ();

		for (int i = 0; i < node.ChildSize(); ++i) {
			Ossia.Node child = node.GetChild (i);
			if (child.GetName() == "position") {
				for (int j = 0; j < child.ChildSize(); ++j) {
					Ossia.Node leaf = child.GetChild(j);
					Transform t = createdgo.transform;
					Ossia.Address addr;
					Ossia.Value oval;
					float val;

					try {
						addr = leaf.GetAddress();
						Debug.Log(addr);
					}
					catch (Exception e) {
						Debug.Log ("Can't get address: " + e.Message);
						return;
					}

					try {
						oval = addr.GetValue();
					}
					catch (Exception e) {
						Debug.Log ("Can't get value: " + e.Message);
						return;
					}

					try {
						val = oval.GetFloat();
					}
					catch (Exception e) {
						Debug.Log ("Can't get float: " + e.Message);
						return;
					}

					Debug.Log (val);

					switch (leaf.GetName ()) {
					case "x": 
						{
							createdgo.transform.position = new Vector3(val, t.position.y, t.position.z);
							break;
						}
					case "y": 
						{
							createdgo.transform.position = new Vector3(t.position.x, val, t.position.z);
							break;
						}
					case "z": 
						{
							createdgo.transform.position = new Vector3(t.position.x, t.position.y, val);
							break;
						}
					default:
						break;
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
			 
		string jsontext = System.IO.File.ReadAllText (jsonname);
		Debug.Log (jsontext);

		p = new Preset (jsontext);

		try {
			Debug.Log("Loaded preset " + p.ToString() + " (" + p.Size() + " elements)");
		}
		catch (Exception e) {
			Debug.Log (e.Message);
		}

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

			IntPtr res;
			BlueYetiAPI.ossia_devices_to_string(dev_ptr, &res);
			Debug.Log("Before applying: " + Marshal.PtrToStringAuto(res));

			//IntPtr dummyProtocol = Ossia.Network.ossia_protocol_local_create ();
			//IntPtr dummyDevice = Ossia.Network.ossia_device_create (dummyProtocol, "dummy");

			p.ApplyToDevice(local_device, false);
			BlueYetiAPI.ossia_devices_to_string(dev_ptr, &res);
			Debug.Log("After applying: " + Marshal.PtrToStringAuto(res));

			bool endOfInstances = false;
			int currentInstance = 1;
			int numberOfInstances = 2;
			while (!endOfInstances && currentInstance <= numberOfInstances) {
				string currentIntanceKeys = "/" + local_device.GetName() + "/scene/cube." + currentInstance.ToString();
				try {
					Debug.Log("Fetching node " + currentIntanceKeys);
					IntPtr fetchednode; 
					ossia_preset_result_enum code = BlueYetiAPI.ossia_devices_get_node(local_device.GetDevice(), currentIntanceKeys, &fetchednode);
					if (code != ossia_preset_result_enum.OSSIA_PRESETS_OK) {
						Debug.Log(code);
						endOfInstances = true;
					}
					else {
						createCube(new Ossia.Node(fetchednode));
					}
				}
				catch (Exception e) {
					Debug.Log("Error occured: " + e.Message);
					endOfInstances = true;
				}
				++currentInstance;
			}
		}
		catch (Exception e) {
			Debug.Log (e.Message);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnApplicationQuit() {
		p.Free ();
	}
}
