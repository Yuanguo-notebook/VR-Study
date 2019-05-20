using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCoor : MonoBehaviour {

	private GameObject b;
	Vector3 vertice;
	string cubeName ;
	// Use this for initialization
	void Start () {
		
		b = gameObject;
		cubeName = b.name;
		vertice = b.transform.position;
		SphereCollider sc = b.GetComponent<SphereCollider> ();
		print (cubeName + " center: "+ sc.transform.position +", radius: "+sc.radius);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
