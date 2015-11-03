using UnityEngine;
using System.Collections;

enum BossAttack
{
    JUMP,
    SWING,
    THROW,
    RAIN,
    MOVE
}
public class bossBehaviour : MonoBehaviour {
    GameObject balls;
    GameObject player;
    public GameObject PBalls;
    public GameObject ballsDropped;
    Rigidbody2D body;
    bool jump;
    float timer = 0;
    BossAttack Attack;
    float coolDown;
    int choose;
    bool isAlive;
    public GameObject explode;
	// Use this for initialization
	void Start () 
    {
        
        balls = GameObject.FindWithTag("bossBalls");
        player = GameObject.FindWithTag("Player");
        body = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
          coolDown += Time.deltaTime;
          if (coolDown >= 5.0f)
          {
              float rayLength = 0.1f;
              jump = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.5f), -Vector2.up, rayLength);
              if (jump)
              {
                  choose =  Random.Range(0, 4);
                  attackSelect(choose);
                  switch (Attack)
                  {
                      case BossAttack.JUMP:
                          {

                              sackJump();
                              coolDown = 0;
                              break;
                          }
                      case BossAttack.SWING:
                          {
                              sackLasso();
                              coolDown = 0;
                              break;
                          }
                      case BossAttack.THROW:
                          {
                              sackThrow();
                              coolDown = 0;
                              break;
                          }
                      case BossAttack.RAIN:
                          {
                              ballRain();
                              coolDown = 0;
                              break;
                          }
                      case BossAttack.MOVE:
                          {
                              enemyMove();
                              coolDown = 0;
                              break;
                          }
                  }


                  jump = false;

              }
          }
        if (!balls.GetComponent<BossBalls>().isVisible)
        {
            timer += Time.deltaTime;
            if (timer>=1)
            {
                balls.GetComponent<BossBalls>().Show();
            }
        }


	}


    void sackJump()
    {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) >= 1)
            {
                if (gameObject.transform.position.x < player.transform.position.x)
                {
                    body.velocity = new Vector2(2, 10);
                   }
               else
                {
                    body.velocity = new Vector2(-2, 10);
                   }
            }
    }


    void sackThrow()
    {
 
        balls.GetComponent<CircleCollider2D>().enabled = false;
        balls.GetComponent<SpriteRenderer>().enabled = false;
       
        balls.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        Instantiate(PBalls,balls.transform.position, balls.transform.rotation);
        balls.GetComponent<BossBalls>().Hide();
           

        
        
    }


    void enemyMove()
    {
        if (Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition) >= 1f)
        {
            if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }
        }
    }


    void attackSelect(int choice)
    {
        if (choice == 0)
        {
            Attack = BossAttack.THROW;
        }
        else if (choice == 1)
        {
            Attack = BossAttack.RAIN;
        }
        else if (choice == 2)
        {
            Attack = BossAttack.SWING;
        }
        else if (choice == 3)
        {
            Attack = BossAttack.JUMP;
        }
        else if (choice == 4)
        {
            Attack = BossAttack.MOVE;
        }
    }
    void ballRain()
{
           float randX = player.transform.localPosition.x;
           for (int i = 0; i < 3; i++)
           {
               Instantiate(ballsDropped).transform.localPosition = new Vector3(randX, 15, 0);
               randX -= 4.5f;
           }
           if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
           {
               gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
           }
           else
           {
               gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
           }
}
    void sackLasso()
    {
        {
          
            balls.GetComponent<SpringJoint2D>().enabled = false;
            if (Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition) >= 1f)
            {
                if (player.transform.localPosition.x > gameObject.transform.localPosition.x)
                {
                   balls.GetComponent<Rigidbody2D>().AddForce(new Vector2(350, 0));
                }

                else
                {
                   balls.GetComponent<Rigidbody2D>().AddForce(new Vector2(-350, 0));
                }
            }
            if (Vector3.Distance(balls.transform.localPosition, gameObject.transform.localPosition) >= 3.5f)
            {
                balls.GetComponent<SpringJoint2D>().enabled = true;
            }
        }
    }
    public void Kill()
    {

        Instantiate(explode).transform.position = gameObject.transform.localPosition;
        gameObject.transform.DetachChildren();
        gameObject.SetActive(false);
        isAlive = false;

    }
}
