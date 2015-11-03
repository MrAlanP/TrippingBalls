using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	public GameObject boss;
	public PlayerHUD playerHUD;

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.GetComponent<Player> ()) {
			//boss.Activate();
            Instantiate(boss, new Vector3(236.54f, 7, 0),new Quaternion(0,0,0,0));
			playerHUD.ShowBossHealth();
		}
	}
}
