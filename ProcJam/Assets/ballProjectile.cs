﻿using UnityEngine;
using System.Collections;

public class ballProjectile : MonoBehaviour 
{
    public GameObject boss;
     GameObject player;
    public bool fire;
    float fireTime;
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
            //gameObject.transform.SetParent(null);
           // Vector2 direction;
            //if (Vector3.Distance(gameObject.transform.localPosition, player.transform.localPosition)>=1)
            //{
            //    direction = new Vector2(1, 0);
            //}
            //else
            //{
            //    direction = new Vector2(-1, 0);
            //}
            
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
        if (hit.collider == GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>())
        {
           Destroy(gameObject);
        }
        else if (hit.collider == GameObject.FindGameObjectWithTag("RubberBand").GetComponent<Collider2D>())
        {
            Destroy(gameObject);
        }
        
    }

}