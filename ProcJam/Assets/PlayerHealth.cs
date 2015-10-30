using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider slider;

	bool isDead;


	// Use this for initialization
	void Start () {

		currentHealth = startingHealth;
	
	}
	
	public void takeDamage(int amount){

		currentHealth -= amount;

		slider.value = currentHealth;

		if (currentHealth <= 0 && !isDead) {

			isDead = true;
		}


	}
}
