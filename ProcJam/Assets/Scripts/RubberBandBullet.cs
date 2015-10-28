using UnityEngine;
using System.Collections;

public class RubberBandBullet : MonoBehaviour {
	
	float speed = 5.0f;
	bool isAlive = false;
	Rigidbody2D body;

	enum BandState{
		Projectile,
		Pickup
	}

	BandState bandState = BandState.Projectile;
	// Use this for initialization
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		transform.localEulerAngles = new Vector3(0,0,Mathf.Rad2Deg*Mathf.Atan2 (body.velocity.y, body.velocity.x));

	}

	public void Shoot(float angle){
		bandState = BandState.Projectile;
		transform.localEulerAngles = new Vector3 (0, 0, Mathf.Rad2Deg*angle);
		body.velocity = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * 10.0f;
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.tag != "Player" && col.tag !="RubberBand") {
			if(bandState == BandState.Projectile){
				ChangeToPickup();
			}
		}

	}

	void ChangeToPickup(){
		bandState = BandState.Pickup;
		body.isKinematic = true;
		body.velocity = new Vector2 (0, 0);
	}

	void Enable(){
		gameObject.SetActive (true);
		isAlive = true;
	}

	void Disable(){
		gameObject.SetActive (false);
		isAlive = false;
	}
}
