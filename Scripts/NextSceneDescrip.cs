using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//created by Yuanguo Lang
//08/13/2018
//All Rights Reserved.
public class NextSceneDescrip : MonoBehaviour {


	private GameObject scoreText;
	private Text text;
	//next scene description
	private string description;
	private string nextScenceName;
	private int nextSceneNum = 0;

	void Start () {
		if(SceneManager.GetActiveScene().name != "Score12"){
			scoreText = GameObject.FindWithTag ("scoreText");
			text = scoreText.GetComponent<Text> ();
			nextScenceName = GetNextSceneName ();
			//print ("next scene name is: " + nextScenceName);

			
			if (nextScenceName.Length == 10) {
				nextSceneNum = Int32.Parse (nextScenceName.Substring (nextScenceName.Length - 2));
			} else {
				nextSceneNum = Int32.Parse (nextScenceName.Substring (nextScenceName.Length - 1));
			}
		
		}
	}
	

	void Update () {
		if (SceneManager.GetActiveScene ().name != "Score12") {

			if (nextSceneNum > 1) {
				description = GlobalControl.Instances.sceneDescrip [nextSceneNum - 1];
				text.text = text.text + "\n" + "\n" + "Next Scene Description:" + "\n" + description;

			}
		}

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
