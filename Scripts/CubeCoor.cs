using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCoor : MonoBehaviour {
	private GameObject b;
	Vector3[] vertice = new Vector3[8];
	string cubeName ;
	// Use this for initialization
	void Start () {
		//b = GameObject.Find(cubeName);
		b = gameObject;
		cubeName = b.name;
		vertice = GetColliderVertexPositions (b);
		for (int i = 0; i < vertice.Length; i++) {
			print (cubeName + ": "+vertice [i].ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3[] GetColliderVertexPositions (GameObject obj)  
	{
		BoxCollider b = obj.GetComponent<BoxCollider>(); //retrieves the Box Collider of the GameObject called obj
		Vector3[] vertices = new Vector3[8];
		vertices[0] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
		vertices[1] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);
		vertices[2] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
		vertices[3] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
		vertices[4] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f);
		vertices[5] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f);
		vertices[6] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f);
		vertices[7] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f);

		return vertices;
	}


}
