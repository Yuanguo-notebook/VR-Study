using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

//created by Yuanguo Lang
//05/10/2018
//All Rights Reserved.
public class ScoreCanvas : MonoBehaviour {
	
	public int count;


	private bool is_triggerred_before_last_scene;
	private bool is_triggerred_after_last_scene;
	private bool is_suppose_to_shoot_last_scene;

	private GameObject scoreText;

	//display the current score
	private Text text;

	string performancePath = "Assets/TimeData/performance.txt";
	bool writeCheck = false;
	string performanceStr;


	// Use this for initialization
	void Start () {
		
		is_triggerred_before_last_scene = GlobalControl.Instances.isTriggeredBefore;
		is_triggerred_after_last_scene = GlobalControl.Instances.isTriggeredAfter;

		is_suppose_to_shoot_last_scene = GlobalControl.Instances.isSupposeToShoot;


		scoreText = GameObject.FindWithTag ("scoreText");
		text = scoreText.GetComponent<Text> ();

		if (is_triggerred_before_last_scene) {
			count = -20;
		} else {
			//is user suppose to shoot
			if (is_suppose_to_shoot_last_scene) {
				//if user shoot, get 10 
				if (is_triggerred_after_last_scene) {
					count = 10;
				}
			//is user didnot shoot , -40
				else {
					count = -40;
				}


			}
			//if user not suppose to shoot
			else {
				//shoot a person user not suppose to shoot
				if (is_triggerred_after_last_scene) {
					count = -20;
				}
				//not shoot a person when user not suppose to shoot
				else {
					count = 5;
				}
			}

		}

		GlobalControl.Instances.scoreCount = GlobalControl.Instances.scoreCount + count;
		print("count: "+count);
	}
	
	// Update is called once per frame
	void Update () {
		string points = "Accuracy Rating: " + GlobalControl.Instances.scoreCount;
		text.text = "<size=60>"+ points+"</size>" ;
		//print ("here is update");
		if (!writeCheck) {
			writePerformance ();
			writeCheck = true;
			//Debug.Log ("write into file" + SceneManager.GetActiveScene ().name + ", time: "+Time.time);
		}
	}


	public void writePerformance(){

		if (is_triggerred_before_last_scene) {
			performanceStr = "shoot early";
		} else if (is_triggerred_after_last_scene) {
			performanceStr = "shoot";
		} else if (!is_triggerred_after_last_scene) {
			performanceStr = "did not shoot";
		}


		//write into file
		Scene scene = SceneManager.GetActiveScene();
		string sceneName = scene.name;
		StreamWriter writer = new StreamWriter(performancePath, true);
		string word = "";
		if (is_suppose_to_shoot_last_scene) {
			word = "suppose to shoot";
		} else {
			word = "not suppose to shoot";
		}
		writer.WriteLine (sceneName +"("+word + ")" +  " : "+performanceStr +";" + " current score: "+GlobalControl.Instances.scoreCount);

		writer.Close();


	}


}
