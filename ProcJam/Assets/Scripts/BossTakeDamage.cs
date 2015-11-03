using UnityEngine;
using System.Collections;

public class BossTakeDamage : MonoBehaviour {

	float iframecount = 0.1f;

	void Awake(){
		
		
	}

	void Update(){

		if (iframecount < 0.1f) {

			iframecount += Time.deltaTime;
		}

	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		if (iframecount >= 0.1f) {
				
			if (coll.gameObject.tag == "bossBalls") {
				BossHealth bossHealth = coll.gameObject.GetComponent<BossHealth>();
				if(bossHealth != null){
					bossHealth.takeDamage(10);
					iframecount = 0f;
				}

			}
		}
	}
	
	
	
	
}
