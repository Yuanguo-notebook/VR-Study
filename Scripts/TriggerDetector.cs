using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactive360
{

public class TriggerDetector : MonoBehaviour {

		public bool isTrigger ;
		public GameObject controllerRight;

		private SteamVR_TrackedObject trackedObj;
		private SteamVR_Controller.Device device;

		private SteamVR_TrackedController controller;
	
		private int scoreToAdd;
		GameManager gameManager;
		string sceneName;
	

		//audio
		public AudioClip shotSound;
		private AudioSource audio;
		private float shotTime = 0.7f;
		public bool timeout = false;

	// Use this for initialization
	void Start () {
			isTrigger = false;

	    gameManager = FindObjectOfType<GameManager> ();
		controller = controllerRight.GetComponent<SteamVR_TrackedController> ();
		controller.TriggerClicked += TriggerPressed;
		trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject> ();
		audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
			if (timeout) {
				controller.enabled = false;
			}

	}

	private void TriggerPressed(object sender, ClickedEventArgs e){
		audio.PlayOneShot(shotSound, shotTime);
			//disable the controller after been pressed
			controller.enabled = false;
			LoadNextScene ();
		
	}

	

	//load the next scene by defining the "scene name" that is to be loaded
	void LoadNextScene() {
		gameManager.SelectScene (GetNextSceneName()); 
	}



	public static string GetNextSceneName()
	{
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













}

}
