using System;
using System.Reflection;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ossia;

namespace AssemblyCSharp
{

	public class OssiaTransform
	{
		Ossia.Node pos_node;
		Ossia.Node orient_node;
		Ossia.Node scale_node;

		Ossia.Node pos_x_node;
		Ossia.Node pos_y_node;
		Ossia.Node pos_z_node;

		Ossia.Address pos_x_addr;
		Ossia.Address pos_y_addr;
		Ossia.Address pos_z_addr;

		Ossia.Node rot_w_node;
		Ossia.Node rot_x_node;
		Ossia.Node rot_y_node;
		Ossia.Node rot_z_node;

		Ossia.Address rot_w_addr;
		Ossia.Address rot_x_addr;
		Ossia.Address rot_y_addr;
		Ossia.Address rot_z_addr;

		Ossia.Node scale_x_node;
		Ossia.Node scale_y_node;
		Ossia.Node scale_z_node;

		Ossia.Address scale_x_addr;
		Ossia.Address scale_y_addr;
		Ossia.Address scale_z_addr;

		public OssiaTransform(GameObject obj, Ossia.Node object_node)
		{
			{
				pos_node = object_node.AddChild ("position");
				pos_x_node = pos_node.AddChild ("x");
				pos_y_node = pos_node.AddChild ("y");
				pos_z_node = pos_node.AddChild ("z");

				pos_x_addr = pos_x_node.CreateAddress (Ossia.ossia_type.FLOAT);
				pos_y_addr = pos_y_node.CreateAddress (Ossia.ossia_type.FLOAT);
				pos_z_addr = pos_z_node.CreateAddress (Ossia.ossia_type.FLOAT);

				pos_x_addr.PushValue (ValueFactory.createFloat (obj.transform.position.x));
				pos_y_addr.PushValue (ValueFactory.createFloat (obj.transform.position.y));
				pos_z_addr.PushValue (ValueFactory.createFloat (obj.transform.position.z));
			}

			{
				orient_node = object_node.AddChild ("rotation");
				rot_w_node = orient_node.AddChild ("w");
				rot_x_node = orient_node.AddChild ("x");
				rot_y_node = orient_node.AddChild ("y");
				rot_z_node = orient_node.AddChild ("z");

				rot_w_addr = rot_w_node.CreateAddress (Ossia.ossia_type.FLOAT);
				rot_x_addr = rot_x_node.CreateAddress (Ossia.ossia_type.FLOAT);
				rot_y_addr = rot_y_node.CreateAddress (Ossia.ossia_type.FLOAT);
				rot_z_addr = rot_z_node.CreateAddress (Ossia.ossia_type.FLOAT);

				rot_w_addr.PushValue (ValueFactory.createFloat (obj.transform.rotation.w));
				rot_x_addr.PushValue (ValueFactory.createFloat (obj.transform.position.x));
				rot_y_addr.PushValue (ValueFactory.createFloat (obj.transform.position.y));
				rot_z_addr.PushValue (ValueFactory.createFloat (obj.transform.position.z));
			}

			{
				scale_node = object_node.AddChild ("scale");
				scale_x_node = scale_node.AddChild ("x");
				scale_y_node = scale_node.AddChild ("y");
				scale_z_node = scale_node.AddChild ("z");

				scale_x_addr = scale_x_node.CreateAddress (Ossia.ossia_type.FLOAT);
				scale_y_addr = scale_y_node.CreateAddress (Ossia.ossia_type.FLOAT);
				scale_z_addr = scale_z_node.CreateAddress (Ossia.ossia_type.FLOAT);

				scale_x_addr.PushValue (ValueFactory.createFloat (obj.transform.localScale.x));
				scale_y_addr.PushValue (ValueFactory.createFloat (obj.transform.localScale.y));
				scale_z_addr.PushValue (ValueFactory.createFloat (obj.transform.localScale.z));
			}
		}


		public void ReceiveUpdates(GameObject obj)
		{
			{
				var pos = obj.transform.position;

				using (var x_val = pos_x_addr.PullValue ()) {
					if (x_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						pos.x = x_val.GetFloat ();
					}
				}

				using (var y_val = pos_y_addr.PullValue ()) {
					if (y_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						pos.y = y_val.GetFloat ();
					}
				}

				using (var z_val = pos_z_addr.PullValue ()) {
					if (z_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						pos.z = z_val.GetFloat ();
					}
				}

				obj.transform.position = pos;
			}

			{
				var rot = obj.transform.rotation;
				using (var w_val = rot_w_addr.PullValue ()) {
					if (w_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						rot.w = w_val.GetFloat ();
					}
				}

				using (var x_val = rot_x_addr.PullValue ()) {
					if (x_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						rot.x = x_val.GetFloat ();
					}
				}

				using (var y_val = rot_y_addr.PullValue ()) {
					if (y_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						rot.y = y_val.GetFloat ();
					}
				}

				using (var z_val = rot_z_addr.PullValue ()) {
					if (z_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						rot.z = z_val.GetFloat ();
					}
				}

				obj.transform.rotation = rot;
			}


			{
				var scale = obj.transform.localScale;

				using (var x_val = scale_x_addr.PullValue ()) {
					if (x_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						scale.x = x_val.GetFloat ();
					}
				}

				using (var y_val = scale_y_addr.PullValue ()) {
					if (y_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						scale.y = y_val.GetFloat ();
					}
				}

				using (var z_val = scale_z_addr.PullValue ()) {
					if (z_val.GetOssiaType () == Ossia.ossia_type.FLOAT) {
						scale.z = z_val.GetFloat ();
					}
				}

				obj.transform.localScale = scale;
			}

		}

		public void SendUpdates(GameObject obj)
		{
			{
				var pos = obj.transform.position;
				using (var val = ValueFactory.createFloat (pos.x)) {
					pos_x_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (pos.y)) {
					pos_y_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (pos.z)) {
					pos_z_addr.PushValue (val);
				}
			}

			{
				var rot = obj.transform.rotation;
				using (var val = ValueFactory.createFloat (rot.w)) {
					rot_w_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (rot.x)) {
					rot_x_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (rot.y)) {
					rot_y_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (rot.z)) {
					rot_z_addr.PushValue (val);
				}
			}

			{
				var scale = obj.transform.localScale;
				using (var val = ValueFactory.createFloat (scale.x)) {
					scale_x_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (scale.y)) {
					scale_y_addr.PushValue (val);
				}
				using (var val = ValueFactory.createFloat (scale.z)) {
					scale_z_addr.PushValue (val);
				}
			}
		}
	}

	internal class OssiaEnabledParameter
	{
		public OssiaEnabledComponent parent;

		public FieldInfo field;
		public Ossia.Expose attribute;

		public Ossia.Node ossia_node;
		public Ossia.Address ossia_address;

		public OssiaEnabledParameter(FieldInfo f, Ossia.Expose attr)
		{
			field = f;
			attribute = attr;
		}

		public void ReceiveUpdates(GameObject obj)
		{
			using (var val = ossia_address.PullValue ()) {
				try {
					field.SetValue (parent.component, val.ToObject());
				}
				catch(Exception) {
				}
			}
		}

		public void SendUpdates(GameObject obj)
		{
			using (var val = ValueFactory.createFromObject (field.GetValue(parent.component))) {
				ossia_address.PushValue (val);
			}
		}
	}
	internal class OssiaEnabledMessage
	{
		public OssiaEnabledComponent parent;

		public MethodInfo field;
		public Ossia.Message attribute;

		public Ossia.Node ossia_node;
		public Ossia.Address ossia_address;

		public OssiaEnabledMessage(MethodInfo f, Ossia.Message attr)
		{
			field = f;
			attribute = attr;
		}

		public void ReceiveUpdates(GameObject obj)
		{
		}

		public void SendUpdates(GameObject obj)
		{
		}

 		public void callback(Ossia.Value aValue) 
		{
			Debug.Log ("CALLBACKED");
			field.Invoke (parent.component, new object[]{});
		}
	}


	internal class OssiaEnabledComponent
	{
		public Component component;
		public Ossia.Node component_node;

		public List<OssiaEnabledParameter> parameters;
		public List<OssiaEnabledMessage> messages;

		public OssiaEnabledComponent(Component comp, Ossia.Node node)
		{
			component = comp;
			component_node = node;
		}


		public void ReceiveUpdates(GameObject obj)
		{
			foreach (var parameter in parameters) {
				parameter.ReceiveUpdates (obj);
			}
		}

		public void SendUpdates(GameObject obj)
		{
			foreach (var parameter in parameters) {
				parameter.SendUpdates (obj);
			}
		}
	}

	public class OssiaObject : MonoBehaviour
	{
		public bool ReceiveUpdates;
		public bool SendUpdates;


		Ossia.Node scene_node;
		Ossia.Node child_node;

		OssiaTransform ossia_transform;

		List<OssiaEnabledComponent> ossia_components = new List<OssiaEnabledComponent>();


		public OssiaObject ()
		{
		}

		void RegisterComponent(Component component)
		{
			List<OssiaEnabledParameter> nodes = new List<OssiaEnabledParameter>();

			List<OssiaEnabledMessage> message_nodes = new List<OssiaEnabledMessage>();
			Debug.Log ("Registering component: " + component.GetType().ToString());
			if (component.GetType () == typeof(UnityEngine.Transform)) {
				return;
			}
			FieldInfo[] fields = component.GetType().GetFields();
			MethodInfo[] methods2 = component.GetType ().GetMethods(BindingFlags.Public | BindingFlags.Instance);
			/*
			if (component.GetType () == typeof(UnityEngine.Tree)) {
				UnityEngine.Tree t = (UnityEngine.Tree)component;

				FieldInfo[] f2 = t.data.GetType ().GetFields ();

				foreach (FieldInfo field in f2) {
					Debug.Log ("========== Tree.Data field: " + field.Name);

						var root = field.GetValue (t.data);

					if (field.Name == "branchGroups") {
						Debug.Log((UnityEditorInternal.TreeEditor.TreeGroupBranch[])field.GetValue (t.data));
					}
					FieldInfo[] f3 = root.GetType ().GetFields ();

					foreach (FieldInfo subf in f3) {
						Debug.Log (field.Name + " field: " + subf.Name);
						if (subf.Name == "rootField") {
							subf.SetValue (t.data, 1.0);
						}

					}

				}
			}
			*/


			// Find the fields that are marked for exposition
			foreach (FieldInfo field in fields) {
				if(Attribute.IsDefined(field, typeof(Ossia.Expose))) {
					var attr = (Ossia.Expose) Attribute.GetCustomAttribute(field, typeof(Ossia.Expose));
					nodes.Add(new OssiaEnabledParameter(field, attr));
				}
			}

			foreach (MethodInfo method in methods2) {
				
				if(Attribute.IsDefined(method, typeof(Ossia.Message))) {
					var attr = (Ossia.Message) Attribute.GetCustomAttribute(method, typeof(Ossia.Message));
					message_nodes.Add(new OssiaEnabledMessage(method, attr));
					Debug.Log ("FOUUUUND THE METHOD: " + method.Name);
				}

			}



			// Create a node for the component
			if (nodes.Count > 0 || message_nodes.Count > 0) {
				OssiaEnabledComponent ossia_c = new OssiaEnabledComponent (component, child_node.AddChild (component.GetType ().ToString ()));
			

			if (nodes.Count > 0) {
				// Create nodes for all the fields that were exposed
				foreach (OssiaEnabledParameter oep in nodes) {
					oep.parent = ossia_c;
					oep.ossia_node = ossia_c.component_node.AddChild (oep.attribute.ExposedName);
					//Debug.Log (oep.field.MemberType.ToString () + " " + oep.field.FieldType.ToString () + " " + oep.field.ReflectedType.ToString ());
					oep.ossia_address = oep.ossia_node.CreateAddress (Ossia.Value.TypeToOssia2 (oep.field.FieldType));
					oep.SendUpdates (this.gameObject);
				}
			}

			if (message_nodes.Count > 0) {
				foreach (OssiaEnabledMessage oep in message_nodes) {
					Debug.Log ("Adding attribute : " + oep.attribute.ExposedName);
					oep.parent = ossia_c;
					oep.ossia_node = ossia_c.component_node.AddChild (oep.attribute.ExposedName);
					//Debug.Log (oep.field.MemberType.ToString () + " " + oep.field.FieldType.ToString () + " " + oep.field.ReflectedType.ToString ());
					oep.ossia_address = oep.ossia_node.CreateAddress (ossia_type.IMPULSE);
					oep.ossia_address.AddCallback (oep.callback);
				}
			}

				ossia_c.parameters = nodes;
				ossia_c.messages = message_nodes;
				ossia_components.Add (ossia_c);
			}
		}

		void RegisterObject(GameObject obj)
		{
			child_node = scene_node.AddChild(obj.name);
			ossia_transform = new OssiaTransform (obj, child_node);

			// For each component, we check the public fields.
			// If these fields have the Ossia.Expose attribute,
			// then we create node structures for them.
			Component[] comps = obj.GetComponents <Component>();
			Debug.Log ("There are " + comps.Length + " components");
			foreach (Component component in comps) {
				RegisterComponent (component);
			}
		}

		public void Start()
		{
			var obj = GameObject.Find ("OssiaController");
			if (obj != null) {
				Debug.Log ("Found controller");
				var comp = obj.GetComponent<OssiaDevices> ();
				if (comp != null) {
					Debug.Log ("Found component");
					scene_node = comp.SceneNode ();
					RegisterObject (this.gameObject);
				}
			}
		}

		public void Update()
		{
			if (ReceiveUpdates) {
				ossia_transform.ReceiveUpdates (this.gameObject);
				foreach (var component in ossia_components) {
					component.ReceiveUpdates (this.gameObject);
				}
			}
			StartCoroutine ("SendUpdatesFun");
		}

		public IEnumerator SendUpdatesFun()
		{
			yield return new WaitForEndOfFrame ();
			if (SendUpdates) {
				ossia_transform.SendUpdates (this.gameObject);
				foreach (var component in ossia_components) {
					component.SendUpdates (this.gameObject);
				}
			}
		}

		static void XChangedCallback(Ossia.Value val)
		{
			Debug.Log("OSSIA callback");
			//this.gameObject.transform.position.Set (0, 0, 0);
		}
	}
}

