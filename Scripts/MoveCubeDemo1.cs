using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//created by Yuanguo Lang
//01/22/2019
//All Rights Reserved.
public class MoveCubeDemo1 : MonoBehaviour {



	private GameObject cube;


	private Vector3 vari;

	// Use this for initialization
	void Start () {
		cube = GameObject.Find ("Cube");

		vari = new Vector3(0.0005f, 0.0f, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {


		cube.transform.position += vari;

	}
}
