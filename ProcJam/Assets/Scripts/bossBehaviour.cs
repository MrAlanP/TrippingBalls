using UnityEngine;
using System.Collections;
public enum attack
{
    SWING,
    THROW,
    JUMP,
    POUND,
    NOTHING
}
public class bossBehaviour : MonoBehaviour 
{
    public GameObject player;
    public GameObject ballsDropped;
    public GameObject explode;
    public GameObject sackThrow;
    ballHealth BallHealth;
    float delay = 0;
    GameObject ballSack;
    float force = 350;
    int choose;
    bool jump;
   
    public attack Attack;
    
    float coolDown;
    int health;

	// Use this for initialization
	void Start () 
    {
        BallHealth = gameObject.GetComponentInChildren<ballHealth>();
        coolDown = 0;
        ballSack = gameObject.transform.GetChild(0).gameObject;   
        
	}
	
	// Update is called once per frame
	void Update () 
    {
       
      
       
        if (BallHealth.health > 0)
        {
            coolDown += Time.deltaTime;
            if (coolDown >= 5.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                choose = Random.Range(0, 4);
                attackSelect(choose);
                switch (Attack)
                {
                    case attack.JUMP:
                        {
                            sackJump();
                            coolDown = 0;
                            break;
                        }
                    case attack.SWING:
                        {
                            sackLasso();
                            coolDown = 0;
                            break;
                        }
                    case attack.THROW:
                        {
                            SackThrow();
                            coolDown = 0;
                            break;
                        }
                    case attack.POUND:
                        {
                            ballRain();
                            coolDown = 0;
                            break;
                        }
                    case attack.NOTHING:
                        {
                            enemyMove();
                            coolDown = 0;
                            break;
                        }
                }
            }
        }
        else
        {
            onDeath();
        }
    }
    void SackThrow()
    {
        jump = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 2.0f), -Vector2.up, 0.1f);

        if (jump)
        {
            //GameObject sackThrow = GameObject.FindGameObjectWithTag("enemyBalls");
            ballSack.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            ballSack.SetActive(false);
            Instantiate(sackThrow, new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 1.2f, 0), ballSack.transform.localRotation);

            float waitForIt = 0;
            waitForIt += Time.deltaTime;
            if (waitForIt >= 2)
            {
                sackThrow.GetComponent<ballProjectile>().fire = true;
                waitForIt = 0;
            }
            ballSack.SetActive(true);

            ballSack.transform.localScale = Vector3.Lerp(ballSack.transform.localScale, new Vector3(1, 0.666f, 1), Time.deltaTime * 100);
            choose = 4;
        }
    }

    void sackLasso()
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
        if (Vector3.Distance(ballSack.transform.localPosition, gameObject.transform.localPosition) >= 3.5f)
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
           else if (choice == 3)
           {
               Attack = attack.POUND;
           }
           else if (choice == 4)
           {
               Attack = attack.NOTHING;
           }
       }



       void sackJump()
       {
          
           //float playerDir = player.transform.localPosition.x;
           float rayLength = 0.1f;
           jump = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-1.0f), -Vector2.up, rayLength);
           if (jump)
           {
               if (Vector3.Distance(gameObject.transform.localPosition, player.transform.localPosition) >= 1)
               {
                   if (gameObject.transform.localPosition.x < player.transform.localPosition.x)
                   {
                       gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(250, 2500), new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y));//AddForce(new Vector2(0, 55));
                   }
                   // gameObject.transform.Translate(Vector3.Lerp(gameObject.transform.localPosition, new Vector3(player.transform.position.x, 20, gameObject.transform.localPosition.z), 25 ));//* Time.deltaTime));
                   else
                   {
                       gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-250, 2500), new Vector2(transform.position.x, transform.position.y));//AddForce(new Vector2(0, 55));
                   }
               }
               jump = false;
               if (gameObject.transform.localPosition.y >=3)
               {
                  gameObject.transform.Translate(player.transform.localPosition.x, gameObject.transform.localPosition.y, 0,Space.World);
               }
           }
           choose = 4;
       }
    void ballRain()
       {
                    
           float randX = player.transform.localPosition.x;// Random.Range(-6.22f, 16.46f);
           for (int i = 0; i < 3; i++)
           {
               Instantiate(ballsDropped).transform.localPosition = new Vector3(randX, 14, 0);
               randX -= 4.5f;
           }
           
           choose = 4;
           
       }
    void enemyMove()
    {
        if (Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition) >= 1f)
        {
            if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(25, 0));
                
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-25, 0));
                
            }

        }
    }
}
/*
things to do
 * hit pause
 * 
 * 
 * ATTACKS:
 *  
 *  
 *  sack swing (huge sack swings across the environment)
 * 

*/