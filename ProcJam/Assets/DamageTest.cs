using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {

	PlayerHealth playerHealth;
	int attackDamage = 20;
	GameObject player;

	void Awake(){

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject == player) {

			Debug.Log("PLAYER HIT");	
			playerHealth.takeDamage(attackDamage);

		}
	}


//	void OnTriggerEnter(Collider other){
//
//		Debug.Log("OVERLAP");
//
//		if (other.gameObject == player) {
//
//			Debug.Log("PLAYER HIT");	
//			playerHealth.takeDamage(attackDamage);
//
//		}


//	}


}
