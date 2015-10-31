using UnityEngine;
using System.Collections;

public class RubberBandBullet : MonoBehaviour {

	public Color color;
	float speed = 5.0f;
	bool isAlive = false;
	Rigidbody2D body;
	Animator animator;
	public SpriteRenderer spriteRenderer;


	public enum BandState{
		Disabled,
		Projectile,
		Pickup
	}

	public BandState bandState = BandState.Disabled;

	float currentStateTime = 0;
	// Use this for initialization
	public void Initialise () {
		body = gameObject.GetComponent<Rigidbody2D> ();
		body.isKinematic = true;
		animator = gameObject.GetComponent<Animator> ();

		spriteRenderer.color = RubberBandColours.colours [Random.Range (0, RubberBandColours.colours.Length)];
		color = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		currentStateTime += Time.deltaTime;
        

		switch (bandState) {
		case BandState.Projectile:{
			transform.localEulerAngles = new Vector3(0,0,Mathf.Rad2Deg*Mathf.Atan2 (body.velocity.y, body.velocity.x));
			break;
		}
		case BandState.Pickup:{
			if(!body.isKinematic){
				if(currentStateTime>1f){
					if(body.velocity == Vector2.zero){
						body.isKinematic = true;
					}
				}
			}

			break;
		}
		}


	}

	public void Shoot(float angle, float power){
		Enable ();
		currentStateTime = 0;
		animator.SetTrigger ("StartScaling");
		body.isKinematic = false;
		bandState = BandState.Projectile;
		transform.localEulerAngles = new Vector3 (0, 0, Mathf.Rad2Deg*angle);
		body.velocity = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * power;
	}
	void OnCollisionEnter2D(Collision2D col){
		switch (bandState) {
		case BandState.Projectile:{
			animator.SetTrigger ("EndScaling");
			Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
			if(enemy!=null){
				enemy.Hit();
			}
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
		currentStateTime = 0;
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
