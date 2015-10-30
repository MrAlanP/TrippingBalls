using UnityEngine;
using System.Collections;

public class ballHealth : MonoBehaviour {

    public int health;
    public GameObject explode;
    public GameObject rubber;
	// Use this for initialization
	void Start () 
    {
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
        Debug.Log(health);
	
	}
    void OnCollisionEnter2D(Collision2D ouch)
    {
        if (ouch.collider.tag == "RubberBand"&& !ouch.transform.GetComponent<Rigidbody2D>().isKinematic)// && ouch.transform.GetComponent<RubberBandBullet>().bandState==RubberBandBullet.BandState.Projectile)
        {
            health -= 1;
        }
    }
}
