using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	Camera cam;

	private Vector3 forwardDir;
	Vector3 dir;
	void Start()
	{
		cam = GetComponent<Camera>();

		 dir = new Vector3(0,0,1.0f);

	}

	void Update()
	{
		forwardDir = transform.TransformDirection(dir) * 1000;

		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position,forwardDir, out hit))
			print("I'm looking at " + hit.point);
		else
			print("I'm looking at nothing!");
		
		Debug.DrawRay(transform.position, forwardDir, Color.green);
	}
}
