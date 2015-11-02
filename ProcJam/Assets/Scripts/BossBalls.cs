using UnityEngine;
using System.Collections;

public class BossBalls : MonoBehaviour {

	int health = 10;
	[HideInInspector]
	public bool isAlive = true;
	[HideInInspector]
	public bool isVisible = true;
	public AudioSource bossHurt;

	public Boss boss;

	SpriteRenderer spriteRenderer;
	BoxCollider2D collider;

	float scaleTimer = 0;


	bool doesDamage = true;
	// Use this for initialization
	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		collider = gameObject.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (scaleTimer < 1) {
			float scale = Mathf.Lerp(0,1,scaleTimer);
			scaleTimer += Time.deltaTime;
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);

		}
	}

//	public void Hurt(){
//		if(isAlive){
//			bossHurt.Play ();
//			health--;
//		
//			if(health<=0){
//				isAlive = false;
//				boss.Kill();
//			}
//		}
//	}


	public void Hide(){
		collider.enabled = false;
		spriteRenderer.enabled = false;
		isVisible = false;
	}

	public void Show(){
		collider.enabled = true;
		spriteRenderer.enabled = true;
		gameObject.transform.localScale = new Vector3 (0, 0, 0);
		scaleTimer = 0;
		isVisible = true;
	}

	void OnCollisionEnter2D(Collision2D hit)
	{
		if (doesDamage) {
			if (hit.collider) {
				Player player = hit.collider.GetComponent<Player>();
				if(player!=null){
					player.Hurt();
					doesDamage = false;
				}
			}
		}
		
		
	}
}

