using UnityEngine;
using System.Collections;

public class enemy1 : MonoBehaviour 
{
    public Transform player;
    float playerTransX;
    float speed;
	// Use this for initialization
	void Start () 
    {
        speed = 2.50f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(player.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

        if (Vector3.Distance(player.localPosition, gameObject.transform.localPosition)>1f)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
           // GetComponent<Rigidbody2D>().velocity.Set(speed * Time.deltaTime,0);
          //  GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Time.deltaTime * 20, 0));
        }

        //Vector3 newEnemyTrans = gameObject.transform.localPosition;
        //playerTransX = player.transform.localPosition.x;
        // if (playerTransX >= gameObject.transform.localPosition.x)
        // {
        //     newXEnemy -= Time.deltaTime;
        // }
        // else if (playerTransX <= gameObject.transform.localPosition.x)
        // {
        //     newXEnemy += Time.deltaTime;
        // }
        // newXEnemy *= speed; 

        // gameObject.transform.localPosition.Set(newXEnemy, newEnemyTrans.y, newEnemyTrans.z);
    }
}
