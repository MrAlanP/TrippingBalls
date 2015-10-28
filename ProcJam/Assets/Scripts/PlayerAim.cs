using UnityEngine;
using System.Collections;

public class PlayerAim : MonoBehaviour {

	bool isAiming = false;

	public Player player;
	public Camera mainCam;

	SpriteRenderer spriteRenderer;

	float angle = 0;


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


		Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePlayerOffset = mousePos - new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);

		angle = Mathf.Atan2 (mousePlayerOffset.y, mousePlayerOffset.x);

		Vector2 aimPos = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle));
		gameObject.transform.localPosition = new Vector3 (aimPos.x, aimPos.y, 0)*0.8f;

		if (Input.GetMouseButtonDown (0)) {
			Shoot();
		}

	}

	public bool GetIsAiming(){
		return isAiming;
	}

	void Shoot(){
		GameObject newBand = Instantiate(Resources.Load<GameObject>("Prefabs/RubberBand"));
		Vector2 spawnPos = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * 0.1f;
		newBand.transform.localPosition = player.transform.localPosition + new Vector3 (spawnPos.x, spawnPos.y, 0);
		RubberBandBullet rubberBand = newBand.GetComponent<RubberBandBullet> ();
		rubberBand.Shoot (angle);

	}
	
}
