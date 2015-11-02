using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	bool isPaused = false;
	public AudioSource GameMusic;
	public float MusicVolume;


	void Start(){
		//MusicVolume = GlobalAudioController.Instance.MusicVolume;
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
		GlobalAudioController.Instance.MusicVolume = MusicVolume;
	}
}
