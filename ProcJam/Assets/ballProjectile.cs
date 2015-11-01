using UnityEngine;
using System.Collections;

public class ballProjectile : MonoBehaviour 
{
    public GameObject boss;
     GameObject player;
    public bool fire;
    float fireTime;

	bool doesDamage = true;
	// Use this for initialization
	void Start () 
    {
        fire = false;
        fireTime = 3.0f;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
       // gameObject.transform.SetParent(boss.transform,true);
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
        {
            gameObject.transform.Rotate(new Vector3(0,0,1) * 90 * (25 * Time.deltaTime));
        }
        if (player.transform.localPosition.x < gameObject.transform.localPosition.x)
        {
            gameObject.transform.Rotate(new Vector3(0,0,-1) * 90 * (25 * Time.deltaTime));
        }
        fireTime -= Time.deltaTime;
        if (fireTime<=0)
        {
            fire = true;
        }
        if (fire)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

            
                if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 0));
                   
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 0));
                   
                }

            
            
            //gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(800, 0), direction);
            //float factor = Time.deltaTime * 1000;
            //this.transform.Translate(direction.x * factor, 0, 0);
            Destroy(gameObject, 3.0f);
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
