using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	PlayerHealth playerHealth;
	public int attackDamage = 20;
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
	
	

	
}
