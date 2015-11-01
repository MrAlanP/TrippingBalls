using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	public Boss boss;

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.GetComponent<Player> ()) {
			boss.Activate();
		}
	}
}
