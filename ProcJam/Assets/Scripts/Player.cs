using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public PlayerAim playerAim;

	Rigidbody2D body;
	SpriteRenderer spriteRenderer;


	float movementSpeed = 4.0f;

	bool onGround = false;

	int collisionCount = 0;


	public enum ControlType{
		Keyboard,
		Controller
	}

	public ControlType controlType = ControlType.Keyboard;

	// Use this for initialization
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement ();
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



}
