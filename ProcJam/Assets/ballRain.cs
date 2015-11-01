using UnityEngine;
using System.Collections;

public class ballRain : MonoBehaviour {

    public GameObject balls;
    public GameObject boss;
    GameObject player;
    bossBehaviour BossScript;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () 
    {
        float time = 0;
        time += Time.deltaTime;
        if (time<=0.8)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (gameObject.transform.localScale.x == 0.1f)
        {
            Destroy(gameObject);
        }
	}
  void OnCollisionEnter2D(Collision2D bang)
    {
      if (bang.collider)
      {
         // gameObject.transform.localScale = (Vector3.Lerp(new Vector3(2, 2, 2), new Vector3(0.1f, 0.1f, 0.1f),Time.deltaTime * 100));
      }
    }
}
