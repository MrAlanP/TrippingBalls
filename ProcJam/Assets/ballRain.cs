using UnityEngine;
using System.Collections;

public class ballRain : MonoBehaviour {

    public GameObject balls;
    public GameObject boss;
    GameObject player;
	bool doesDamage = true;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () 
    {
        float time = 0;
        time += Time.deltaTime;
        if (time<=0.8)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (gameObject.transform.localScale.x == 0.1f)
        {
            Destroy(gameObject);
        }
	}
  void OnCollisionEnter2D(Collision2D hit)
    {
		if (doesDamage) {
			if (hit.collider) {
				Player player = hit.collider.GetComponent<Player>();
				if(player!=null){
					player.Hurt();
					doesDamage = false;
					Destroy(gameObject, 3.0f);
				}
				
				RubberBandBullet rb = hit.collider.GetComponent<RubberBandBullet>();
				if(rb!=null){
					Destroy(gameObject);
					doesDamage = false;
				}
			}
		}
    }
}
