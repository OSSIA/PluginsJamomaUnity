using UnityEngine;
using System.Collections;
using Ossia;
using UnityEngine.Internal;
public class TreeController : MonoBehaviour {

	[Ossia.Expose("tree_freq")]
	public float TreeFrequency;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		var data = this.gameObject.GetComponent<UnityEngine.Renderer> ();
		data.materials [1].SetFloat ("_InvFade", TreeFrequency / 1000);
	}
}
