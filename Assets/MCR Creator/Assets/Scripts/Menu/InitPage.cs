// Description : Init Game to prevent bug when the game came back from a track to the main menu 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitPage : MonoBehaviour {
	[Header("Duration before launching the next scene.")] 
	public float WaitDuration = 2;

	public Image splashScreen;
	
	
	// Use this for initialization
	void Awake () {
		StartCoroutine ("F_WaitBeforeLaunchingGame");
	}

	IEnumerator F_WaitBeforeLaunchingGame() {
		yield return new WaitForSeconds (WaitDuration);		// Wait before launching the new scene

		PlayerPrefs.SetInt ("WeAreOnTrack",0);				// Init the Main Menu
		SceneManager.LoadScene (1);							// Load the main Menu
	}
}
