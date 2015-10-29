using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public PlayerHUD playerHUD;
	public GameObject projectiles;
	public PlayerAim playerAim;

	Rigidbody2D body;
	SpriteRenderer spriteRenderer;


	float movementSpeed = 4.0f;

	float previousFireAxis = 0;

	bool isGrounded = false;

   	
	List<GameObject> rubberBands = new List<GameObject>();



	public enum ControlType{
		Keyboard,
		Controller
	}

	public ControlType controlType = ControlType.Keyboard;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i<10; i++) {
			SpawnRubberBand();
		}


		body = gameObject.GetComponent<Rigidbody2D> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controlType == ControlType.Keyboard) {
			if(Input.GetJoystickNames()[0]!=""){
				controlType = ControlType.Controller;
				playerAim.SetSpriteRendererActive(false);
			}
		}

		UpdateMovement ();
		UpdateFiring ();
	}
	

	void FixedUpdate(){
		isGrounded = false;
	}

	void OnCollisionStay2D(Collision2D col){
		if (body.velocity.y > 3) {
			isGrounded = false;
			return;
		}
		foreach (ContactPoint2D contacts in col.contacts) {
			if(Vector3.Angle(Vector3.up, contacts.normal) < 60){
				isGrounded = true;
			}
		}
	}

	void UpdateMovement(){

		float horizontalMovement = Input.GetAxis ("Horizontal");

		

		if (horizontalMovement != 0) {
			if(horizontalMovement<0){
				horizontalMovement = -1;
			}
			else{
				horizontalMovement = 1;
			}

			body.velocity = new Vector2 (horizontalMovement * movementSpeed, body.velocity.y) * Time.deltaTime * 60;

		} 
		else {
			body.velocity = new Vector2(0, body.velocity.y);
		}

		if (isGrounded) {
			if (Input.GetAxis ("Jump") != 0) {
				//isGrounded = false;
				body.AddForce(new Vector2(0,250));
			}
		}


	}

	void UpdateFiring(){

		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
		
	}


	public void Shoot(){

		if (rubberBands.Count>0) {
			float aimAngle = playerAim.GetAngle();
			Vector2 spawnPos = new Vector2 (Mathf.Cos (aimAngle), Mathf.Sin (aimAngle)) * 0.1f;
			rubberBands[rubberBands.Count-1].transform.localPosition = new Vector3 (spawnPos.x, spawnPos.y, 0);
			rubberBands[rubberBands.Count-1].transform.SetParent (projectiles.transform);
			RubberBandBullet rubberBand = rubberBands[rubberBands.Count-1].GetComponent<RubberBandBullet> ();
			rubberBand.Shoot (aimAngle);

			rubberBands.RemoveAt(rubberBands.Count-1);
			playerHUD.UpdateRubberBandsCount (rubberBands.Count);
		}
		
		
	}

	void SpawnRubberBand(){
		GameObject newBand = Instantiate(Resources.Load<GameObject>("Prefabs/RubberBand"));

		newBand.GetComponent<RubberBandBullet> ().Disable ();
		AddRubberBandAmmo (newBand);
	}

	public void AddRubberBandAmmo(GameObject rubberBand){
		rubberBand.tag = "PlayerRubberBand";
		rubberBand.gameObject.layer = LayerMask.NameToLayer("PlayerRubberBand");
		rubberBand.transform.SetParent (transform);
		rubberBands.Add (rubberBand);
		playerHUD.UpdateRubberBandsCount (rubberBands.Count);
	}





}
