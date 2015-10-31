using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	const int START_HEALTH = 3;
	int currentHealth;
	public Slider healthSlider;

	public SpriteRenderer helmetSprite;

	public Sprite[] helmetDamageSprites;

	bool isDead;


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

			isDead = true;
		}


	}
}
