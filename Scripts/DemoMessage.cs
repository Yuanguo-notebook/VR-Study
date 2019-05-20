using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//created by Yuanguo Lang
//08/13/2018
//All Rights Reserved.
public class DemoMessage : MonoBehaviour {
	public int count;


	private bool is_triggerred_before_last_scene;
	private bool is_triggerred_after_last_scene;
	private bool is_suppose_to_shoot_last_scene;

	private GameObject scoreText;

	//display the current score
	private Text text;


	private AudioSource audio;
	//0: 1st shoot
	//1: 1st not shoot
	//2: 2nd shoot
	//3: 2nd not shoot
	//4: 2nd shoot early
	public AudioClip[] voice = new AudioClip[5];

	int voiceMark = -1;
	int sceneNum = 0;
	string currentSceneName;


	// Use this for initialization
	void Start () {
		audio = GameObject.FindWithTag ("scoreText").GetComponent<AudioSource> ();
		currentSceneName = SceneManager.GetActiveScene ().name;
		sceneNum = Int32.Parse(currentSceneName.Substring (currentSceneName.Length - 1));

		is_triggerred_before_last_scene = GlobalControl.Instances.isTriggeredBefore;
		is_triggerred_after_last_scene = GlobalControl.Instances.isTriggeredAfter;

		is_suppose_to_shoot_last_scene = GlobalControl.Instances.isSupposeToShoot;


		scoreText = GameObject.FindWithTag ("scoreText");
		text = scoreText.GetComponent<Text> ();

		//shoot early
		if (is_triggerred_before_last_scene) {
			count = -20;
			if (sceneNum == 2) {
				voiceMark = 4;
				//print ("current scene: " + sceneNum + " with voice mark: " + voiceMark);
			}
		} else {
			//is user suppose to shoot
			if (is_suppose_to_shoot_last_scene) {
				//if user shoot, get 10 
				if (is_triggerred_after_last_scene) {
					count = 10;
					voiceMark = 2;
				}
				//is user didnot shoot , -40
				else {
					count = -40;
					voiceMark = 3;
				}


			}
			//if user not suppose to shoot
			else {
				//shoot a person user not suppose to shoot
				if (is_triggerred_after_last_scene) {
					count = -20;
					voiceMark = 0;
				}
				//not shoot a person when user not suppose to shoot
				else {
					count = 5;
					voiceMark = 1;
				}
			}

		}


	}

	// Update is called once per frame
	void Update () {
		text.text =  "Accuracy Rating: " + count.ToString();
		//print (GlobalControl.Instances.sceneDescrip [11]);
		if (voiceMark > -1) {
			if (!audio.isPlaying) {
				audio.PlayOneShot (voice [voiceMark]);
			}
		}
	}
}
