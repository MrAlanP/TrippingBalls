using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour 
{
   public GameObject enemyBounce;
   public GameObject enemy;
   List<GameObject> enemyList;
   int spawnAmount;
   float timer = 0;
	// Use this for initialization
	void Awake ()
    {
        enemyList = new List<GameObject>();
        spawnAmount = Random.Range(1, 3);
	    for (int i =0;i<spawnAmount;i++)
        {
            if (Random.Range(0,10)<6)
            {
                enemy.transform.position = gameObject.transform.position;
                enemyList.Add(enemy);

            }
            else
            {
                enemyBounce.transform.position = gameObject.transform.position;
                enemyList.Add(enemyBounce);
            }
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
        SpawnDelay();
	}
    void SpawnDelay()
    {
        timer += Time.deltaTime;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (timer > 2.0f)
            {
                Instantiate(enemyList[i],gameObject.transform.position,gameObject.transform.rotation);
                enemyList.RemoveAt(i);
                timer = 0;
            }
        }
    }
}
