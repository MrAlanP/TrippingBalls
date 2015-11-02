using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour {

	const int START_HEALTH = 3;
	public int currentHealth;
	public Slider healthSlider;

	public SpriteRenderer helmetSprite;

	public Sprite[] helmetDamageSprites;

	public Animator anim;

	public bool isDead;




	// Use this for initialization
	void Awake () {

		currentHealth = START_HEALTH;
		if (helmetDamageSprites.Length > 0) {
			helmetSprite.sprite = helmetDamageSprites [0];
		}

	}
	
	public void takeDamage(){

		currentHealth--;
		currentHealth = Mathf.Clamp(currentHealth,0,START_HEALTH);
		if (helmetDamageSprites.Length > 0) {
			helmetSprite.sprite = helmetDamageSprites [3-currentHealth];
		}
		healthSlider.value = ((float)currentHealth/(float)START_HEALTH) * 100;

		if (currentHealth <= 0 && !isDead) {

			Death();
		}


	}

	void Death(){

		isDead = true;

		anim.SetBool ("isDead", true);

		//knock the player over
		//make him look dead
		//disable movement
		//disable shooting
		//death message

	}
}
