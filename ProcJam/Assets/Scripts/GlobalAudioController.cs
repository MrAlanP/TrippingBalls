using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlobalAudioController : MonoBehaviour {

	public static GlobalAudioController Instance;
	public float MusicVolume;

	// Use this for initialization
	void Start () {
	
		if (Instance == null) {
			//DontDestroyOnLoad (gameObject);
			Instance = this;
		}
		else if (Instance != this){
			//Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
