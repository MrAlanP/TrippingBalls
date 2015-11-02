using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	public Boss boss;
	public PlayerHUD playerHUD;

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.GetComponent<Player> ()) {
			boss.Activate();
			playerHUD.ShowBossHealth();
		}
	}
}
