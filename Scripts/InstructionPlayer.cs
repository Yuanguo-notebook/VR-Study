using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//created by Yuanguo Lang
//08/20/2018
//All Rights Reserved.
public class InstructionPlayer : MonoBehaviour {


	private AudioSource audio;
	public AudioClip voice;

	// Use this for initialization
	void Start () {
		audio = GameObject.Find ("Instruction").GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying) {
			audio.PlayOneShot (voice);
		}
	}
}
