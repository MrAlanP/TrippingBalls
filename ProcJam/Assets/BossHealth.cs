using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour {
	
	const int START_HEALTH = 100;
	public int currentHealth;
	public Slider healthSlider;
	
	//some sort of colour tint to make his balls blue
		
	bool isDead;

	void Awake () {
		
		currentHealth = START_HEALTH;

	}
	
	public void takeDamage(int amount){

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead) {
			
			isDead = true;
		}
		
		
	}
}
