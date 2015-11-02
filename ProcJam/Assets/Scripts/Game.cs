using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	bool isPaused = false;
	public AudioSource mainMusic;
	public float musicVol = 1;


	// Use this for initialization
	void Awake () {

		mainMusic.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			return;
		}



	}

	public void Pause(){
		isPaused = true;
		mainMusic.Pause ();
	}

	public void Resume(){
		isPaused = false;
		mainMusic.Play ();
	}
}
