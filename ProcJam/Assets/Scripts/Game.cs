using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	bool isPaused = false;
	// Use this for initialization
	void Awake () {



	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			return;
		}



	}

	public void Pause(){
		isPaused = true;
	}

	public void Resume(){
		isPaused = false;
	}
}
