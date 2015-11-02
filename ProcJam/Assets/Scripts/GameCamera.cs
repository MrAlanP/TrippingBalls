using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public Player player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (player!=null) {
			transform.position = new Vector3 (player.transform.position.x+3, player.transform.position.y+1, transform.position.z);
		}

	}
}
