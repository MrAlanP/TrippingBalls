using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Rigidbody2D body;
	SpriteRenderer spriteRenderer;

	float movementSpeed = 4.0f;

	bool onGround = false;
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


			float xScale = horizontalMovement;
			body.velocity = new Vector2 (horizontalMovement * movementSpeed, body.velocity.y) * Time.deltaTime * 60;
			gameObject.transform.localScale = new Vector3(xScale, 1, 1);
		} 
		else {
			body.velocity = new Vector2(0, body.velocity.y);
		}

		if (onGround) {
			if(body.velocity.y<0.01f && body.velocity.y>-0.01f){
				if (Input.GetAxis ("Jump") != 0) {
					body.AddForce(new Vector2(0,250));
					onGround = false;
				}
			}
		}


	}

	void OnCollisionEnter2D(Collision2D col){
		onGround = true;

		float angle = col.gameObject.transform.localEulerAngles.z;

		gameObject.transform.localEulerAngles = new Vector3 (0, 0, angle);
	}



}
