using UnityEngine;
using System.Collections;

public class PlayerAim : MonoBehaviour {

	bool isAiming = false;

	public Player player;
	public Camera mainCam;

	SpriteRenderer spriteRenderer;

<<<<<<< HEAD
	float aimAngle = 0;
=======
	public float aimAngle = 0;
    public float joysensitivityX = 3F;
    public float joysensitivityY = 3F;
>>>>>>> origin/master

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

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

        float joyY = Mathf.Abs(Input.GetAxis("joystickRX"));
        float joyX = Mathf.Abs(Input.GetAxis("joystickRY"));

		Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePlayerOffset = mousePos - new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);

		aimAngle = Mathf.Atan2 (mousePlayerOffset.y, mousePlayerOffset.x);
        //aimAngle = Mathf.Atan2(joyY*joysensitivityY, joyX*joysensitivityX);
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
