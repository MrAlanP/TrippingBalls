using UnityEngine;
using System.Collections;

public class PlayerAim : MonoBehaviour {

	bool isAiming = false;

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool GetIsAiming(){
		return isAiming;
	}
	
}
