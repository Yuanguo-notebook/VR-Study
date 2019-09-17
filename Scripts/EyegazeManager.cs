using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;

namespace Tobii.Research.Unity.Examples
{
	public class EyegazeManager : MonoBehaviour {

		private VREyeTracker _eyeTracker;

		private GameObject target;
		private Vector3 forwardDir;
		private Vector3 posePosition;
		private Vector3 rayWorldDirection;
		public Vector3 targetDis = new Vector3 (1.0f, 0.2f, 0f);

		string eyePath = "Assets/TimeData/eyegaze";

		bool writeCheck = false;

		string eyestr;




		// Use this for initialization
		void Start () {
			_eyeTracker = VREyeTracker.Instance;
			posePosition = _eyeTracker.LatestProcessedGazeData.Pose.Position;

			target = GameObject.Find ("Target");

			target.transform.position = posePosition + targetDis;
			Scene scene = SceneManager.GetActiveScene();
			string sceneName = scene.name;
			string day = System.DateTime.Now.ToString ("yy-MM-dd");
			eyePath = eyePath + "/" + day + "-" +  sceneName + ".txt";

			var file  = File.Open (eyePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

		}



		// Update is called once per frame
		void Update () {
			if (!_eyeTracker)
			{
				return;
			}

			posePosition = _eyeTracker.LatestProcessedGazeData.Pose.Position;
			rayWorldDirection =  _eyeTracker.LatestProcessedGazeData.CombinedGazeRayWorld.direction;
			target.transform.position = posePosition + targetDis;

			forwardDir = transform.TransformDirection(rayWorldDirection) * 1000;

			/**
			 * write into file: 
			 * (posePosition)|(rayWorldDirection)|(hit.point or NA)
			 * */
			var file  = File.Open (eyePath, FileMode.Append, FileAccess.Write);
			StreamWriter writer = new StreamWriter(file);

			string hitstr = "";

			RaycastHit hit;
			if (Physics.Raycast (posePosition, forwardDir, out hit) && hit.collider == target.GetComponent<Collider> () ) {
				print ("I'm looking at " + hit.point);
				hitstr = hit.point.ToString ();

			} else {
				print ("NA");
				hitstr = "NA";
			}




			writer.WriteLine (posePosition + "|" + rayWorldDirection + "|" + hitstr);


			writer.Close();



		}





	}



}
