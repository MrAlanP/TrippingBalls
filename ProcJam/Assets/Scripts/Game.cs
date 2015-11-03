using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	bool isPaused = false;
	public AudioSource GameMusic;
	public float MusicVolume;
	public Slider MusicVolSlider;

	void Start(){

		MusicVolume = MusicVolSlider.value;
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
	//	if (Input.GetAxis ("Cancel")!=0) {
	//		Application.LoadLevel("Game");
	//	}
	}

	public void Pause(){
		isPaused = true;
	}

	public void Resume(){
		isPaused = false;
	}
}
