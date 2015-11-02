using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	bool isPaused = false;
	public AudioSource GameMusic;
	public float MusicVolume;
	GlobalAudioController audioController;

	void Start(){
		audioController = gameObject.GetComponent<GlobalAudioController> ();
		MusicVolume = audioController.MusicVolume;
	}

	// Use this for initialization
	void Awake () {

		GameMusic.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			return;
		}
		if (Input.GetAxis ("Cancel")!=0) {
			//Application.LoadLevel("MainMenu");
		}
	}

	public void Pause(){
		isPaused = true;
		GameMusic.Pause ();
	}

	public void Resume(){
		isPaused = false;
		GameMusic.Play ();
	}

	public void SaveSettings(){
		audioController.MusicVolume = MusicVolume;
	}
}
