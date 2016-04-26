using UnityEngine;
using System.Collections;
using Ossia;
using UnityEngine.Internal;

public class MobController : MonoBehaviour {

	private GameObject MyObject;
	[Ossia.Expose("create_mobs")]
	public bool instantiate = false;

	// Use this for initialization
	void Start () {
		MyObject = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		MyObject.AddComponent<Rigidbody> ();
		MyObject.AddComponent<SphereCollider> ();
	}

	// Update is called once per frame
	void Update () {
		if (instantiate) {

			var actual_object = Instantiate (MyObject);
			actual_object.transform.position = new Vector3 (0, 10, 10);
		}
		instantiate = false;
	}
}
/*
public class MobController : MonoBehaviour {

	private GameObject MyObject;
	bool instantiate = false;

	// Use this for initialization
	void Start () {
		MyObject = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		MyObject.AddComponent<Rigidbody> ();
		MyObject.AddComponent<SphereCollider> ();
	}

	[Ossia.Message("create_mob")]
	public void CreateAMob() 
	{
		instantiate = true;
	}

	// Update is called once per frame
	void Update () {
		if (instantiate) {

			var actual_object = Instantiate (MyObject);
			actual_object.transform.position = new Vector3 (0, 10, 10);
		}
		instantiate = false;
	}
}
*/