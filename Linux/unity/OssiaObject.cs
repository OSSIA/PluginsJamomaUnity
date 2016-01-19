using System;
using UnityEngine;
using UnityEditor;
using Ossia;

namespace AssemblyCSharp
{
	public class OssiaObject : MonoBehaviour 
	{
		Ossia.Node scene_node;
		Ossia.Node child_node;
		Ossia.Node x_node;
		Ossia.Node y_node;
		Ossia.Node z_node;

		Ossia.Address x_addr;
		Ossia.Address y_addr;
		Ossia.Address z_addr;

		public OssiaObject ()
		{
		}


		void RegisterObject(GameObject obj)
		{
			child_node = scene_node.AddChild(obj.name);
			x_node = child_node.AddChild ("x");
			y_node = child_node.AddChild ("y");
			z_node = child_node.AddChild ("z");

			x_addr = x_node.CreateAddress (Ossia.ossia_type.FLOAT);
			y_addr = y_node.CreateAddress (Ossia.ossia_type.FLOAT);
			z_addr = z_node.CreateAddress (Ossia.ossia_type.FLOAT);
		}

		public void Start()
		{
			var obj = GameObject.Find ("OssiaController");
			if (obj != null) {
				var comp = obj.GetComponent<OssiaDevices> ();
				if (comp != null) {
					scene_node = comp.SceneNode ();
				}
			}
		}

		public void Update()
		{
			var pos = this.gameObject.transform.position;

		}
	}
}

