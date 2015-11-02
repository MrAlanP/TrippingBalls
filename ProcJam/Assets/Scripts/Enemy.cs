using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    Collider2D floor;
    Transform player;
    float playerTransX;
    public float speed = 3.5f;

	Rigidbody2D body;
	SpriteRenderer spriteRenderer;

	public enum EnemyState{
		Idle,
		Chase
	}

	EnemyState enemyState = EnemyState.Idle;
	// Use this for initialization
	void Awake () 
    {
		body = gameObject.GetComponent<Rigidbody2D> ();
        player = GameObject.FindGameObjectWithTag("Player").transform;
		enemyState = EnemyState.Chase;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!spriteRenderer.isVisible) {
			return;
		}
        //transform.LookAt(player.position);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

		//Moving up
		switch (enemyState) {
		case EnemyState.Chase:{
				//if (body.velocity.y > 0) {

					
					if (Vector3.Distance(player.localPosition, gameObject.transform.localPosition) >= 1f)
					{
						if(player.localPosition.x>gameObject.transform.localPosition.x){
							body.AddForce(new Vector2(speed,0));
                            gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 1)* Time.deltaTime * (speed*Mathf.PI));
						}
						else
                        {
							body.AddForce(new Vector2(-speed,0));
                            gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, -1) * Time.deltaTime * (speed*Mathf.PI));
						}
						
					}
					
				//}
				break;
			}

		}
        
        
        

    }

    public void Hit()
    {
		enemyState = EnemyState.Idle;

    }
}
