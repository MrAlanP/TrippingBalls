using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour {
	
	const int START_HEALTH = 100;
	public int currentHealth;
	public Slider healthSlider;
    public GameObject explode;
    public BossBalls bossBalls;
	public GameObject trigger;
	BossTrigger bossTrigger;

    float bleedReset = 0.2f;
	
	//some sort of colour tint to make his balls blue
		
	bool isDead;

	void Awake () {

		trigger = GameObject.FindGameObjectWithTag("Trigger");

		bossTrigger = trigger.GetComponent<BossTrigger>();

		if(bossTrigger == null){

			Debug.Log("fuck everything");
		}

		currentHealth = START_HEALTH;
		healthSlider = bossTrigger.healthSlider;

	}

    void Update()
    {
        if (bleedReset < 0.2f)
        {
            bleedReset += Time.deltaTime;
            if (bleedReset >= 0.2f)
            {
               // bossBalls.bloodParticles.Stop();
            }
        }
        
    }
	public void takeDamage(int amount){

		currentHealth -= amount;

		healthSlider.value = currentHealth;
		bossBalls.Bleed ();
        //bossBalls.bloodParticles.Play();
        bleedReset = 0;

		if (currentHealth <= 0 && !isDead) {
			
			Death();
		}
		
		
	}

	
	void Death(){
		
		isDead = true;
        Instantiate(explode).transform.position = gameObject.transform.position;
        Destroy(gameObject);
		GameObject.FindGameObjectWithTag("Boss").GetComponent<bossBehaviour>().Kill();
		//knock the boss over
		//make him look dead
		//disable movement
		//disable shooting
		//victory message
		
	}
}
