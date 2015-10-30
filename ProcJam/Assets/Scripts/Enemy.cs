using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    Collider2D floor;
    Transform player;
    float playerTransX;
    float speed;

	Rigidbody2D body;

	public enum EnemyState{
		Idle,
		Chase
	}

	EnemyState enemyState = EnemyState.Idle;
	// Use this for initialization
	void Awake () 
    {
		body = gameObject.GetComponent<Rigidbody2D> ();
        speed = 3.50f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
		enemyState = EnemyState.Chase;
	}
	
	// Update is called once per frame
    void Update()
    {
        
        //transform.LookAt(player.position);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

		//Moving up
		switch (enemyState) {
		case EnemyState.Chase:{
				//if (body.velocity.y > 0) {
					if (Vector3.Distance(player.localPosition, gameObject.transform.localPosition) >= 1f)
					{
						if(player.localPosition.x>gameObject.transform.localPosition.x){
							body.AddForce(new Vector2(5,0));
                            gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 1)* Time.deltaTime * (speed*Mathf.PI));
						}
						else{
							body.AddForce(new Vector2(-5,0));
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
