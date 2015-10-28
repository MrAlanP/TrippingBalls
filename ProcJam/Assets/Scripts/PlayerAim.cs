using UnityEngine;
using System.Collections;

public class PlayerAim : MonoBehaviour {

	bool isAiming = false;

	public Player player;
	public Camera mainCam;

	SpriteRenderer spriteRenderer;


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
		}
	}

	void UpdateKeyboardAiming(){


		Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);// new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
		Vector2 mousePlayerOffset = mousePos - new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);

		float angle = Mathf.Atan2 (mousePlayerOffset.y, mousePlayerOffset.x);
		Debug.Log (mousePlayerOffset);

		Vector2 aimPos = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle));
		gameObject.transform.localPosition = new Vector3 (aimPos.x, aimPos.y, 0)*0.8f;

	}

	public bool GetIsAiming(){
		return isAiming;
	}
	
}
