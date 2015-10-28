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

	bool onGround = false;

	int collisionCount = 0;

   

	int rubberBands = 10;


	public enum ControlType{
		Keyboard,
		Controller
	}

	public ControlType controlType = ControlType.Keyboard;

	// Use this for initialization
	void Awake () {
		playerHUD.UpdateRubberBandsCount (rubberBands);
		body = gameObject.GetComponent<Rigidbody2D> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement ();

		if (Input.GetButtonDown("Fire1")) 
		{
			Shoot();
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

		if (onGround) {
			if (Input.GetAxis ("Jump") != 0) {
				body.AddForce(new Vector2(0,250));
				onGround = false;
			}
		}


	}

	void OnCollisionEnter2D(Collision2D col){
		onGround = true;
		collisionCount++;
	}

	void OnCollisionExit2D(Collision2D col){
		collisionCount--;
		if (collisionCount == 0) {
			onGround = false;
		}

	}

	void Shoot(){
		if (rubberBands>0) {
			GameObject newBand = Instantiate(Resources.Load<GameObject>("Prefabs/RubberBand"));
			Vector2 spawnPos = new Vector2 (Mathf.Cos (playerAim.aimAngle), Mathf.Sin (playerAim.aimAngle)) * 0.1f;
			newBand.transform.SetParent (projectiles.transform);
			newBand.transform.localPosition = transform.localPosition + new Vector3 (spawnPos.x, spawnPos.y, 0);
			RubberBandBullet rubberBand = newBand.GetComponent<RubberBandBullet> ();
			rubberBand.Shoot (playerAim.aimAngle);
			rubberBands--;
			playerHUD.UpdateRubberBandsCount (rubberBands);
		}
		
		
	}





}
