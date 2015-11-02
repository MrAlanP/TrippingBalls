using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	PlayerHealth playerHealth;
	public PlayerHUD playerHUD;
	public RubberBandCounter rubberBandCounter;
	public GameObject projectiles;
	public GameObject playerSprite;
	public PlayerGrapple playerGrapple;
	public SpriteRenderer rubberBandSprite;
	public ParticleSystem rubberBandSnapParticles;
	public PlayerAim playerAim;
	public AudioSource fire;
	public AudioSource jump;
	public AudioSource rubberBandPickup;
	public AudioSource hurt;

	Rigidbody2D body;

	public Animator anim;
	float moveAnim;

	float movementSpeed = 4.0f;

	float previousFireAxis = 0;

	bool isGrounded = false;
	bool canWalkLeft = true;
	bool canWalkRight = true;

   	
	public List<GameObject> rubberBands = new List<GameObject>();



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
		UpdateRubberBandColour ();


		playerHealth = gameObject.GetComponent<PlayerHealth> ();
		body = gameObject.GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (controlType == ControlType.Keyboard) {
			if(Input.GetJoystickNames().Length>0){
				if(Input.GetJoystickNames()[0]!=""){
					controlType = ControlType.Controller;
				}
			}

		}
		UpdateMovement ();
	}
	

	void FixedUpdate(){
		isGrounded = false;
		canWalkLeft = true;
		canWalkRight = true;

		RaycastHit2D right = Physics2D.Raycast (transform.position + new Vector3(0.1f,0,0), new Vector2 (1, 0), 0.03f);
		RaycastHit2D left = Physics2D.Raycast (transform.position - new Vector3(0.2f,0,0), new Vector2 (-1, 0), 0.03f);
		if (right) {
			if(right.collider.gameObject.tag!="Player"){
				canWalkRight = false;
			}

		}
		if (left) {
			if(left.collider.gameObject.tag!="Player"){
				canWalkLeft = false;
			}
			
		}
	}

	void OnCollisionStay2D(Collision2D col){


		if (body.velocity.y > 3f) {
			isGrounded = false;
			return;
		}
		foreach (ContactPoint2D contacts in col.contacts) {
			if (Vector3.Angle (Vector3.up, contacts.normal) < 60) {

				isGrounded = true;
			}
		}
	}


	void UpdateMovement(){

		float horizontalMovement = Input.GetAxis ("Horizontal");


		if (horizontalMovement != 0) {
			float xScale = 1;
			if(horizontalMovement<0){
				horizontalMovement = -1;
				xScale = -1;
				if(!canWalkLeft){
					horizontalMovement = 0;
				}
			}
			else{
				horizontalMovement = 1;
				if(!canWalkRight){
					horizontalMovement = 0;
				}
			}

			playerSprite.transform.localScale = new Vector3(xScale,1,1);

			float xVel = horizontalMovement * movementSpeed * Time.deltaTime * 60;
			body.velocity = new Vector2(xVel,body.velocity.y);



		} 
		else {
			body.velocity = new Vector2(0, body.velocity.y);
		}

		if (isGrounded) {
			if (Input.GetAxis ("Jump") != 0) {
				isGrounded = false;
				body.AddForce(new Vector2(0,250));
				jump.Play();
			}
		}


		if (Input.GetAxis ("Grapple") != 0) {
			UseGrapple ();
		} 
		else {
			if (playerGrapple.grappleActive) {
				playerGrapple.DisableGrapple();
			}
		}


		
		


		anim.SetFloat ("Speed",  Mathf.Abs(body.velocity.x));

	}



	public void Shoot(float power){

		if (rubberBands.Count>0) {
			float aimAngle = playerAim.GetAngle();
			Vector2 spawnPos = new Vector2 (Mathf.Cos (aimAngle), Mathf.Sin (aimAngle)) * 0.1f;
			rubberBands[rubberBands.Count-1].transform.localPosition = rubberBandSprite.gameObject.transform.localPosition;
			rubberBands[rubberBands.Count-1].transform.SetParent (projectiles.transform);
			RubberBandBullet rubberBand = rubberBands[rubberBands.Count-1].GetComponent<RubberBandBullet> ();
			rubberBand.Shoot (aimAngle, power);

			fire.Play();

			rubberBands.RemoveAt(rubberBands.Count-1);
			playerHUD.UpdateRubberBandsCount (rubberBands.Count);
			rubberBandCounter.UpdateRubberBandsCount (rubberBands.Count);
			UpdateRubberBandColour ();
		}
		
		
	}

	void SpawnRubberBand(){
		GameObject newBand = Instantiate(Resources.Load<GameObject>("Prefabs/RubberBand"));
		RubberBandBullet rb = newBand.GetComponent<RubberBandBullet> ();
		rb.Initialise ();
		rb.Disable ();
		AddRubberBandAmmo (newBand);
	}

	void UpdateRubberBandColour(){
		if (rubberBands.Count > 0) {
			rubberBandSprite.color = rubberBands [rubberBands.Count-1].GetComponent<RubberBandBullet> ().color;
		}

	}

	public void AddRubberBandAmmo(GameObject rubberBand){
		rubberBand.tag = "PlayerRubberBand";
		rubberBand.gameObject.layer = LayerMask.NameToLayer("PlayerRubberBand");
		rubberBand.transform.SetParent (transform);
		rubberBands.Add (rubberBand);

		rubberBandPickup.Play ();

		playerHUD.UpdateRubberBandsCount (rubberBands.Count);
		rubberBandCounter.UpdateRubberBandsCount (rubberBands.Count);
	}


	public void SnapRubberBand(){
		GameObject rubberBand = rubberBands [rubberBands.Count - 1];
		rubberBandSnapParticles.startColor = rubberBand.GetComponent<RubberBandBullet> ().color;
		rubberBandSnapParticles.Emit (2);
		rubberBands.Remove (rubberBand);
		playerHUD.UpdateRubberBandsCount (rubberBands.Count);
		rubberBandCounter.UpdateRubberBandsCount (rubberBands.Count);
		UpdateRubberBandColour ();

	}

	public void AddForce(Vector3 force){
		body.AddForce (force);
	}

	public void Hurt(){
		playerHealth.takeDamage ();
		hurt.Play ();
	}

	public void UseGrapple(){
		playerGrapple.UseGrapple ();
	}




}
