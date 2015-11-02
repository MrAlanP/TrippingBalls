using UnityEngine;
using System.Collections;

public class BossTakeDamage : MonoBehaviour {

	void Awake(){
		
		
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
				
		if (coll.gameObject.tag == "Boss") {


			BossHealth bossHealth = coll.gameObject.GetComponent<BossHealth>();
			bossHealth.takeDamage(10);

		}
	}
	
	
	
	
}
