using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive360;

//created by Yuanguo Lang
//05/10/2018
//All Rights Reserved.
public class ScoreHolder : MonoBehaviour {

	public bool suppose_to_shoot;

	private GameObject controller;
	private CountdownTimer timer;

	//count down the time left for this secene
	public float timerCount = 35f;

	private float endTime = 0.0f;

	private AudioSource audio;
	public AudioClip hurtSound;

	private TriggerDetector detector;




	// Use this for initialization  
	void Start () {
		
		controller = GameObject.FindWithTag ("rightController");
		timer = controller.GetComponent<CountdownTimer> ();

		audio = GameObject.FindWithTag("controller").GetComponent<AudioSource> ();
		detector = GameObject.FindGameObjectWithTag ("controller").GetComponent<TriggerDetector> ();


	}
	
	// Update is called once per frame
	void Update () {

		GlobalControl.Instances.isTriggeredBefore = timer.triggered_before;
		GlobalControl.Instances.isTriggeredAfter = timer.triggered_after;
		GlobalControl.Instances.isSupposeToShoot = suppose_to_shoot;

		timerCount -= Time.deltaTime;

		if (timerCount <= endTime && suppose_to_shoot) {
			if (!audio.isPlaying) {
				
				audio.PlayOneShot (hurtSound, 1.0f);
			}
		}


	}
}
