using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
//using UnityEditor;
using UnityEngine.Video;


//created by Yuanguo Lang
//05/10/2018
//All Rights Reserved.
namespace Interactive360
{
	//player/controller(right)
public class CountdownTimer : MonoBehaviour {

		public bool triggered_before;
		public bool triggered_after;
	
		//controller
		private SteamVR_TrackedObject trackedObject;
		private SteamVR_Controller.Device device;


		//scene
		Scene activeScene;
		private int sceneToLoad;
		Scene sceneLoading;



		//ScoreKeeper scoreKeeper;
		//CurrentSceneScore currentSceneScore;
		private int wrongShootScore;
		private int shootScore;
		private int notShootScore;

		//Game Manager
		GameManager gameManager;

		//video player
		private VideoPlayer videoPlayer;



		string reactionTimePath = "Assets/TimeData/time.txt";

		public float gunAppearTimeStamp = 33.0f;

		private float reactTimer = 0.0f;

		//participate's react time from gun/object appears to pulls trigger
		private float reactTime = 0.0f;

		//pause video for 5 seconds before starts playing
		public float endPauseTime = 6.0f;
		private float pauseTimer = 0.0f;
		private bool pauseCheck1 = false;
		private bool pauseCheck2 = false;
		public float startPauseTime = 1.0f;
		public float startPauseTime2 = 0.0f;
		public float endPauseTime2 = 0.0f;
		private bool pauseCheck3 = false;
		private bool pauseCheck4 = false;

		//timer
		public float originalVideoTime = 30f; //each video is exactly 30 seconds long
		private float totalTime;
		private float timer;
		private static float timeout = 0f;
		private static float threshold = 2.0f;  //used to be 2
		private bool isTimeOut;

		private TriggerDetector detector;

		// Use this for initialization
		void Start () {
			
			totalTime = originalVideoTime + endPauseTime - startPauseTime + endPauseTime2 - startPauseTime2;
			timer = totalTime;



			gameManager = FindObjectOfType<GameManager> ();
			trackedObject = GetComponent<SteamVR_TrackedObject> ();
			detector = GameObject.FindGameObjectWithTag ("controller").GetComponent<TriggerDetector> ();


			triggered_before = false;
			triggered_after = false;

			videoPlayer = GameObject.FindGameObjectWithTag ("video").GetComponent<VideoPlayer>();
			pauseCheck1 = false;
			pauseCheck2 = false;
			isTimeOut = false;
			pauseCheck3 = false;
			pauseCheck4 = false;
		}
	

		// Update is called once per frame
		void Update () {


			device = SteamVR_Controller.Input ((int)trackedObject.index);

			//decrement the timer
			timer -= Time.deltaTime;

			//increase react timer
			reactTimer += Time.deltaTime * 1000;

			pauseTimer += Time.deltaTime*10;

			//pause video at 
			if (pauseTimer >= startPauseTime*10 && !pauseCheck1) {
				pauseCheck1 = true;
				pauseVideo ();

			}

			//if pause time is no longer left
			if (pauseTimer >= endPauseTime*10 && !pauseCheck2) {
				pauseCheck2 = true;
				playVideo ();
			}

			//pause video at end
			if (pauseTimer >= startPauseTime2*10 && !pauseCheck3) {
				pauseCheck3 = true;
				pauseVideo ();

			}

			//play video at end
			if (pauseTimer >= endPauseTime2*10 && !pauseCheck4) {
				pauseCheck4 = true;
				playVideo ();
			}


			//when the trigger is pressed before 2 seconds .. 30-2f
			if ( timer > threshold) {


				if(device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
					print ("trigger before 28 seconds");
					device.TriggerHapticPulse (3999); //Haptic Trigger

					triggered_before = true;

				}
			}
			else{
				//At 2 seconds left - 0f
				if (timer <= threshold && (!isTimeOut)) {

					if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
						print ("shoot after 28 seconds");
						//get participate's react time long
						reactTime = reactTimer - gunAppearTimeStamp*1000;
						writeTime (reactTime);

						print ("trigger after ");

						device.TriggerHapticPulse (3999); //Haptic Trigger

						//gameManager.SelectScene (GetNextSceneName());
						isTimeOut = true;
						triggered_after = true;
					}


					//if there is less than Timeout seconds left, 0f
					else if (timer <=timeout && (!isTimeOut)) {
						print ("time out");
						trackedObject.enabled = false;
						detector.timeout = true;
						gameManager.SelectScene (GetNextSceneName());

						isTimeOut = true;
					}

				}

			}





	}

		public void writeTime(float reactTi){
			//write into file
			Scene scene = SceneManager.GetActiveScene();
			string sceneName = scene.name;
			StreamWriter writer = new StreamWriter(reactionTimePath, true);
			writer.WriteLine (sceneName + ": "+reactTi);
			writer.Close();

		}



		IEnumerator LongVibration(float length, float strength){

			for (float i = 0; i < length; i += Time.deltaTime) {



				device.TriggerHapticPulse ((ushort)Mathf.Lerp (0, 3999, strength));
				yield return null; 

			}

		}





			public static string GetNextSceneName()
			{
			//Debug.Log ("getting the next scene name");
				var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

				if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
				{
					return GetSceneNameByBuildIndex(nextSceneIndex);
				}

				return string.Empty;
			}





		public static string GetSceneNameByBuildIndex(int buildIndex)
		{
			return GetSceneNameFromScenePath(SceneUtility.GetScenePathByBuildIndex(buildIndex));
		}

		

			private static string GetSceneNameFromScenePath(string scenePath)
			{
				// Unity's asset paths always use '/' as a path separator
				var sceneNameStart = scenePath.LastIndexOf("/", System.StringComparison.Ordinal) + 1;
				var sceneNameEnd = scenePath.LastIndexOf(".", System.StringComparison.Ordinal);
				var sceneNameLength = sceneNameEnd - sceneNameStart;
				return scenePath.Substring(sceneNameStart, sceneNameLength);
			}



		public void pauseVideo(){
			videoPlayer.Pause ();
		}


		public void playVideo(){
			videoPlayer.Play ();

		}



}



}