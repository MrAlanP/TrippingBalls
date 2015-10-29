using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAim : MonoBehaviour {

	bool isAiming = false;

	public Player player;
	public Camera mainCam;
	public GameObject rubberBand;
	public GameObject TrajectoryPointPrefab;
	float power = 5.0f;

	SpriteRenderer spriteRenderer;


	float aimAngle = 0;

	private int numOfTrajectoryPoints = 30;
	private List<GameObject> trajectoryPoints;
	

	// Use this for initialization
	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

		trajectoryPoints = new List<GameObject>();

		for (int i=0; i < numOfTrajectoryPoints; i++) {

			GameObject dot = (GameObject) Instantiate (TrajectoryPointPrefab);
			dot.GetComponent<Renderer>().enabled = false;
			trajectoryPoints.Insert(i,dot);


		}

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


		if (Input.GetButton ("Fire1")) {
			SetSpriteRendererActive ();
			
			Vector2 mousePos = mainCam.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePlayerOffset = mousePos - new Vector2 (player.transform.localPosition.x, player.transform.localPosition.y);
			
			aimAngle = Mathf.Atan2 (mousePlayerOffset.y, mousePlayerOffset.x);
			SetAimPos (aimAngle);


		} 
		else {
			if(spriteRenderer.enabled){
				player.Shoot();
			}
			SetSpriteRendererActive(false);
		}

			Vector3 vel = GetForceFrom(rubberBand.transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition));
			float angle = Mathf.Atan2(vel.y,vel.x)* Mathf.Rad2Deg;
			transform.eulerAngles = new Vector3(0,0,angle);
			setTrajectoryPoints(transform.position, vel/rubberBand.GetComponent<Rigidbody2D>().mass);



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

	private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos){

		return (new Vector2 (toPos.x, toPos.y) - new Vector2 (fromPos.x, fromPos.y)) * power;

	}

	void setTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity )
	{
		float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
		float angle = Mathf.Rad2Deg*(Mathf.Atan2(pVelocity.y , pVelocity.x));
		float fTime = 0;
		fTime += 0.1f;
		for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
		{
			float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
			Vector3 pos = new Vector3(pStartPosition.x + dx , pStartPosition.y + dy ,2);
			trajectoryPoints[i].transform.position = pos;
			trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
			trajectoryPoints[i].transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude)*fTime,pVelocity.x)*Mathf.Rad2Deg);
			fTime += 0.1f;
		}
	}


	
}
