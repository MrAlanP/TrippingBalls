using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossTrigger : MonoBehaviour {

	public GameObject boss;
	public PlayerHUD playerHUD;
	public Slider healthSlider;

	bool triggered = false;

	void OnTriggerEnter2D(Collider2D col){
		if (triggered) {
			return;
		}
		if (col.gameObject.GetComponent<Player> ()) {
			//boss.Activate();
			triggered = true;
            Instantiate(boss, new Vector3(236.54f, 7, 0),new Quaternion(0,0,0,0));
			playerHUD.ShowBossHealth();
		}
	}
}
