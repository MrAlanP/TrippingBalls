using UnityEngine;
using System.Collections;

public class RubberBandBullet : MonoBehaviour {
	
	float speed = 5.0f;
	Rigidbody2D body;
	// Use this for initialization
	void Awake () {
		body = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles = new Vector3(0,0,Mathf.Rad2Deg*Mathf.Atan2 (body.velocity.y, body.velocity.x));

	}

	public void Shoot(float angle){
		transform.localEulerAngles = new Vector3 (0, 0, Mathf.Rad2Deg*angle);
		body.velocity = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * 10.0f;
	}
}
