using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	

	
	void Awake(){

		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.GetComponent<Player>()) {
			PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
			playerHealth.takeDamage();
		}
	}
	
	

	
}
