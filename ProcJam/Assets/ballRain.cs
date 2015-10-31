using UnityEngine;
using System.Collections;

public class ballRain : MonoBehaviour {

    public GameObject balls;
    public GameObject boss;
    bossBehaviour BossScript;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, 4);
        gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
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
	}
}
