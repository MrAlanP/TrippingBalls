using UnityEngine;
using System.Collections;

public class RubberBandBullet : MonoBehaviour {
	
	float speed = 5.0f;
	bool isAlive = false;
	Rigidbody2D body;
	Animator animator;
	public SpriteRenderer spriteRenderer;

	enum BandState{
		Disabled,
		Projectile,
		Pickup
	}

	BandState bandState = BandState.Disabled;
	// Use this for initialization
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D> ();
		body.isKinematic = true;
		animator = gameObject.GetComponent<Animator> ();

		spriteRenderer.color = RubberBandColours.colours [Random.Range (0, RubberBandColours.colours.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		if (bandState == BandState.Projectile) {
			transform.localEulerAngles = new Vector3(0,0,Mathf.Rad2Deg*Mathf.Atan2 (body.velocity.y, body.velocity.x));
		}


	}

	public void Shoot(float angle){
		Enable ();
		animator.SetTrigger ("StartScaling");
		body.isKinematic = false;
		bandState = BandState.Projectile;
		transform.localEulerAngles = new Vector3 (0, 0, Mathf.Rad2Deg*angle);
		body.velocity = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * 10.0f;
	}
	void OnCollisionEnter2D(Collision2D col){
		switch (bandState) {
		case BandState.Projectile:{
			animator.SetTrigger ("EndScaling");
			ChangeToPickup();
			break;
		}
		case BandState.Pickup:{
			if(col.collider.tag=="Player"){
				Player player = col.collider.GetComponent<Collider2D>().GetComponent<Player>();
				PlayerPickup(player);
			}
			break;
		}
		}
	}


	void ChangeToPickup(){
		bandState = BandState.Pickup;
		tag = "RubberBand";
		gameObject.layer = LayerMask.NameToLayer("RubberBand");
		body.velocity = new Vector2 (0, 0);

	}

	void PlayerPickup(Player player){
		Disable ();
		bandState = BandState.Disabled;
		player.AddRubberBandAmmo (gameObject);
	}

	void Enable(){
		gameObject.SetActive (true);
		isAlive = true;
	}

	public void Disable(){
		gameObject.SetActive (false);
		isAlive = false;
	}
}

public static class RubberBandColours{
	public static Color[] colours = new Color[8] { Color.red, Color.blue, Color.green, Color.magenta, Color.yellow, Color.cyan, Color.white, new Color(1f,0.5f,0)};

}
