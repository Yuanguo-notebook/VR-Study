using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


//created by Yuanguo Lang on 1/31/2019
// All Rights Reserved

namespace Tobii.Research.Unity.Examples
{
	public class DataManager1 : MonoBehaviour {

		private VREyeTracker _eyeTracker;

		private GameObject cube;

		private Vector3 vari;
		private Vector3 posePosition;
		private Vector3 rayWorldDirection;
		private string path = "D:\\PoliceStudy\\demo1\\Assets\\Scenes\\eyetrackScene1\\data1.xml";
		private XmlTextReader xtr;
		private Vector3[] poses;
		private Vector3[] directs;

		private float counter = 0;
		private int c = 0;
		private int i = 0;

		// Use this for initialization
		void Start () {
			xtr  = new XmlTextReader (path);
			createInstance ();
			_eyeTracker = VREyeTracker.Instance;

			cube = GameObject.Find ("Cube1");

			vari = new Vector3(0.0f, 0.0f, 1.0f);
			readxml ();

			for (int j = 0; j < poses.Length; j++) {
				if (directs [j] [0] != 0) {
					print ("j: " + j);
					i = j;
					break;
				}
			}


		}


		// Update is called once per frame
		void Update () {

			counter += Time.deltaTime * 1000000.0f; //0.000001 ;
			if (counter >= 8332 && i < poses.Length) {
//				print ("curr counter" + counter);
//				print ("hhh");
				posePosition = poses[i];
				rayWorldDirection = directs [i];
				c += 1;
				counter = 0;
				i += 1;
//				print (c);
			}


			if (!_eyeTracker)
			{
				return;
			}

//			posePosition = _eyeTracker.LatestProcessedGazeData.Pose.Position;
//			rayWorldDirection =  _eyeTracker.LatestProcessedGazeData.CombinedGazeRayWorld.direction;

			//cube.transform.position = posePosition + rayWorldDirection;
			cube.transform.position =  rayWorldDirection;
		}

		// create 2 arrays to store directiona and pose values
		void createInstance(){
			int count = 0;
			while (xtr.Read ()) {
				if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Pose") {
					count += 1;

				}
			}
			poses = new Vector3[count];
			directs = new Vector3[count];
			print ("count"+ count);
		}

		//read xml values
		void readxml(){

			xtr = new XmlTextReader (path);
			int index = 0;
			while (xtr.Read ()) {
				if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Pose") {
					string s = xtr.GetAttribute ("Position");
//					print ("pose string: "+s);


					poses [index] = tofloatVec3(s);
				}
				//
				if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "CombinedGazeRayWorld") {
					string s2 = xtr.GetAttribute ("Direction");
//					print (s2);
					directs[index] = tofloatVec3(s2);
					index += 1;
				}



			}//while

		}//end function

		//convert string to float vector3
		private Vector3  tofloatVec3(string s){
			s = s.Substring (1, s.Length-2);
//			print ("pose string 2: "+s);
			string[] vec_str = new string[3];

			vec_str = s.Split (new string[]{", "}, System.StringSplitOptions.None);
			//					print (vec_str[0] + "||" + vec_str[1] + "||" + vec_str[2]);
			float[] f = new float[3];
			for (int i = 0; i < vec_str.Length; i++) {
				f [i] = float.Parse (vec_str [i]);
			}
			Vector3 p1 = new Vector3 (f[0], f[1], f[2]);

			//					Debug.Log("log"+p1.ToString("F8"));
			return p1;
		}//end funtion

	}//end class



}






























































