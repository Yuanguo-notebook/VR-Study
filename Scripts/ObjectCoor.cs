using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ObjectCoor : MonoBehaviour {


	private VideoPlayer videoPlayer;

	private bool playcheck = false;

	private float stopTime = 1.0f;
	private float time = 0.0f;


	// Use this for initialization
	void Start () {
		videoPlayer = GameObject.FindGameObjectWithTag ("video").GetComponent<VideoPlayer>();




	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;
		if (time >= stopTime && !playcheck) {
			pauseVideo ();
		}
		
	}


	public void pauseVideo(){
		videoPlayer.Pause ();
	}


	public void playVideo(){
		videoPlayer.Play ();

	}




}
