using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAim : MonoBehaviour {


	public Player player;
	public GameObject rubberBand;
	public GameObject trajectory;
	public GameObject trajectoryPointPrefab;

	float shootPower = 0;
	const float MAX_SHOOT_POWER = 10;

	SpriteRenderer spriteRenderer;


	float aimAngle = 0;
	bool aimingActive = false;
	float aimingActiveTime = 0;

	float powerUpTime = 0.5f;
	float snapTime = 3.0f;



	List<GameObject> trajectoryPoints;
	int numOfTrajectoryPoints = 30;
	

	// Use this for initialization
	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

		trajectoryPoints = new List<GameObject>();

		for (int i=0; i < numOfTrajectoryPoints; i++) {

			GameObject dot = (GameObject) Instantiate (trajectoryPointPrefab);
			dot.transform.SetParent(trajectory.transform);
			trajectoryPoints.Add(dot);


		}

	}
	
	// Update is called once per frame
	void Update () {

		if (player.rubberBands.Count == 0) {
			return;
		}

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
			SetAimingActive ();
			
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePlayerOffset = mousePos - new Vector2 (player.transform.localPosition.x, player.transform.localPosition.y);
			
			aimAngle = Mathf.Atan2 (mousePlayerOffset.y, mousePlayerOffset.x);
			SetAimPos (aimAngle);

			if (aimingActiveTime>3.0f) {
				SetAimingActive(false);
				player.SnapRubberBand();
				return;
			}


		} 
		else {
			if(aimingActive){
				player.Shoot(shootPower);
			}
			SetAimingActive(false);
		}



	}

	void UpdateGamepadAiming(){



		float joyRY = Input.GetAxis("JoystickRY");
		float joyRX = Input.GetAxis("JoystickRX");

		float prevAimAngle = aimAngle;
		bool prevAimingEnabled = aimingActive;


		SetAimingActive(!(Mathf.Abs (joyRX) < 0.3f && Mathf.Abs (joyRY) < 0.3f));
		if (aimingActive) {
			aimAngle = Mathf.Atan2 (joyRY, -joyRX);
			SetAimPos (aimAngle);
		}


		if (aimingActiveTime>3.0f) {
			SetAimingActive(false);
			player.SnapRubberBand();
			return;
		}

		if (prevAimingEnabled && !aimingActive) {
			player.Shoot(shootPower);
		}




	}

	public void SetAimingActive(bool active = true){
		player.rubberBandSprite.enabled = active;
		trajectory.SetActive (active);
		aimingActive = active;

		if (!active) {
			aimingActiveTime = 0;
		}
	}


	void SetAimPos(float angle){
		aimingActiveTime += Time.deltaTime;

		//Set shot power based on time aiming has been down for
		shootPower = Mathf.Lerp (MAX_SHOOT_POWER*0.5f, MAX_SHOOT_POWER, aimingActiveTime / powerUpTime);

		//Set rubberband scale and rotation
		rubberBand.transform.localEulerAngles = new Vector3 (0, 0, (Mathf.Rad2Deg*angle)+10);
		rubberBand.transform.localScale = new Vector3 (Mathf.Lerp (0.5f,1.2f, aimingActiveTime / powerUpTime), 1, 1);

		Vector2 aimPos = new Vector2 (Mathf.Cos (aimAngle), Mathf.Sin (aimAngle));
		gameObject.transform.localPosition = new Vector3 (aimPos.x, aimPos.y, 0)*0.8f;

		setTrajectoryPoints (rubberBand.gameObject.transform.position, new Vector3 (aimPos.x, aimPos.y, 0) * shootPower);

	}

	public float GetAngle(){
		return aimAngle;
	}

	void setTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity)
	{
		float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
		float angle = Mathf.Rad2Deg*(Mathf.Atan2(pVelocity.y , pVelocity.x));
		float fTime = 0;
		fTime += 0.05f;
		for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
		{
			float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
			Vector3 pos = new Vector3(pStartPosition.x + dx , pStartPosition.y + dy ,2);
			trajectoryPoints[i].transform.position = pos;

			trajectoryPoints[i].transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude)*fTime,pVelocity.x)*Mathf.Rad2Deg);
			fTime += 0.06f;
		}
	}
}





	

