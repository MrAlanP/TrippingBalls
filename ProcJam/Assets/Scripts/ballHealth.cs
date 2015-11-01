using UnityEngine;
using System.Collections;

public class ballHealth : MonoBehaviour {

    public int health;
    public GameObject explode;
    public GameObject rubber;
    bossBehaviour BossScript;
	// Use this for initialization
	void Start () 
    {
        BossScript = transform.parent.GetComponent<bossBehaviour>();
        health = 10;
        //gameObject.GetComponent<SpringJoint2D>().transform.localPosition = gameObject.transform.parent.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (health<=0)
        {
            Instantiate(explode).transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }

        if (BossScript.Attack == attack.THROW)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 0.666f, 1), 25 * Time.deltaTime);
        }
	
	}
    void OnCollisionEnter2D(Collision2D ouch)
    {
        if (ouch.collider.tag == "RubberBand" && ouch.relativeVelocity.x > 0) //!ouch.transform.GetComponent<Rigidbody2D>().isKinematic && ouch.relativeVelocity.x > 0)// && ouch.transform.GetComponent<RubberBandBullet>().bandState==RubberBandBullet.BandState.Projectile)
        {
            health -= 1;
        }
        if (ouch.collider.tag == "Player")
        {
            if (!GetComponent<SpringJoint2D>().enabled)
            {
                GetComponent<SpringJoint2D>().enabled = true;
            }
        }
    }
}
