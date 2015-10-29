﻿using UnityEngine;
using System.Collections;

public class PlayerAim : MonoBehaviour {

	bool isAiming = false;

	public Player player;
	public Camera mainCam;
	public GameObject rubberBand;

	SpriteRenderer spriteRenderer;


	float aimAngle = 0;

	

	// Use this for initialization
	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (player.controlType) {
		case Player.ControlType.Keyboard:
			UpdateKeyboardAiming();
			break;
		case Player.ControlType.Controller:
			UpdateGamepadAiming();
			break;
		}

	}

	void UpdateKeyboardAiming(){

		SetSpriteRendererActive ();

		Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePlayerOffset = mousePos - new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);

		aimAngle = Mathf.Atan2 (mousePlayerOffset.y, mousePlayerOffset.x);
		SetAimPos (aimAngle);
        //aimAngle = Mathf.Atan2(joyY*joysensitivityY, joyX*joysensitivityX);

	}

	void UpdateGamepadAiming(){
		float joyRY = Input.GetAxis("JoystickRY");
		float joyRX = Input.GetAxis("JoystickRX");

		float prevAimAngle = aimAngle;
		bool prevAimingEnabled = spriteRenderer.enabled;


		SetSpriteRendererActive(!(Mathf.Abs (joyRX) < 0.3f && Mathf.Abs (joyRY) < 0.3f));
		if (spriteRenderer.enabled) {
			aimAngle = Mathf.Atan2 (joyRY, -joyRX);
			SetAimPos (aimAngle);
		}




		if (prevAimingEnabled && !spriteRenderer.enabled) {
			player.Shoot();
		}




	}

	public void SetSpriteRendererActive(bool active = true){
		spriteRenderer.enabled = active;
		player.rubberBandSprite.enabled = active;
	}


	void SetAimPos(float angle){
		rubberBand.transform.localEulerAngles = new Vector3 (0, 0, (Mathf.Rad2Deg*angle)+10);
		Vector2 aimPos = new Vector2 (Mathf.Cos (aimAngle), Mathf.Sin (aimAngle));
		gameObject.transform.localPosition = new Vector3 (aimPos.x, aimPos.y, 0)*0.8f;
	}

	public float GetAngle(){
		return aimAngle;
	}

	public bool GetIsAiming(){
		return isAiming;
	}


	
}
