using UnityEngine;
using System.Collections;

public class bossBehaviour : MonoBehaviour 
{
    public GameObject player;
    public GameObject explode;
    ballHealth BallHealth;
    float delay = 0;
    public GameObject ballSack;
    float force = 850;
    int choose;
 
    enum attack
    {
        SWING,
        THROW,
        JUMP,
        NOTHING
    }
    attack Attack;
    float coolDown;
    int health;

	// Use this for initialization
	void Start () 
    {
        BallHealth = gameObject.GetComponentInChildren<ballHealth>();
        coolDown = 0;
        
        //balls = gameObject.transform.GetChild(0);
        //balls.transform.
	}
	
	// Update is called once per frame
	void Update () 
    {
       
        onDeath();
        coolDown += Time.deltaTime;
        if (coolDown>=3.0f)
        {
            choose = Random.Range(0, 2);
            coolDown = 0;
        }
         
       attackSelect(choose);
            switch (Attack)
        {
                case attack.JUMP:
                {
                    sackJump();
                    break;
                }
                case attack.SWING:
                {
                    break;
                }
                case attack.THROW:
                {
                    sackThrow();
                    break;
                }
        }
    }
    void sackThrow()
    {
       // ballSack.GetComponent<SpringJoint2D>().frequency = 10;
        ballSack.GetComponent<SpringJoint2D>().enabled = false;
        if (Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition) >= 1f)
        {
            if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
            {
                gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0));
            }

            else
            {
                gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0));
            }  
            
            

        }
        if (Vector3.Distance(ballSack.transform.localPosition, gameObject.transform.localPosition) >= 4.5f)
        {
            ballSack.GetComponent<SpringJoint2D>().enabled = true;
        }
        
        choose = 4;
    }




       void onDeath()
    {
        if (BallHealth.health <= 0)
        {
            delay += Time.deltaTime;
            if (delay >= 1)
            {
                Instantiate(explode).transform.position = gameObject.transform.localPosition;
                gameObject.transform.DetachChildren();
                gameObject.SetActive(false);

            }
        }
    }



       void attackSelect(int choice)
       {
           if (choice == 0)
           {
               Attack = attack.THROW;
           }
           else if (choice == 1)
           {
               Attack = attack.JUMP;
           }
           else if (choice == 2)
           {
               Attack = attack.SWING;
           }
           else
           {
               Attack = attack.NOTHING;
           }
       }



       void sackJump()
       {
          
           float playerDir = player.transform.localPosition.x;
          

          //if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
          //{
          //    playerDir = 10;
          //}
          //else
          //{
          //    playerDir = -10;
          //}
            
           float rayLength = 0.1f;
            bool jump = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-2.0f), -Vector2.up, rayLength);
           if (jump)
           {

               Debug.Log("jumping");
               gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(0,3150),new Vector2(transform.position.x, transform.position.y));//AddForce(new Vector2(0, 55));
               jump = false;
               if (gameObject.transform.localPosition.y >= 80)
               {
                   gameObject.transform.Translate(player.transform.localPosition.x, 100, 0);
               }
           }
           choose = 4;
       }
}
/*
things to do
 * hit pause
 * 
 * 
 * ATTACKS:
 *  
 *  sack jump (boss jumps and falls sack down on roughly the player)
 *  sack swing (huge sack swings across the environment)
 * 

*/