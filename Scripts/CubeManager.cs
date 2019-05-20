using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Tobii.Research.Unity.Examples
{
	public class CubeManager : MonoBehaviour {

		private VREyeTracker _eyeTracker;

		private GameObject cube;

		private Vector3 vari;
		private Vector3 posePosition;
		private Vector3 rayWorldDirection;

		// Use this for initialization
		void Start () {
			_eyeTracker = VREyeTracker.Instance;

			cube = GameObject.Find ("Cube");

			vari = new Vector3(0.0f, 0.0f, 1.0f);


		}
		
		// Update is called once per frame
		void Update () {
			if (!_eyeTracker)
			{
				return;
			}

			posePosition = _eyeTracker.LatestProcessedGazeData.Pose.Position;
			rayWorldDirection =  _eyeTracker.LatestProcessedGazeData.CombinedGazeRayWorld.direction;

			cube.transform.position = posePosition + rayWorldDirection;
		}





	}



}
