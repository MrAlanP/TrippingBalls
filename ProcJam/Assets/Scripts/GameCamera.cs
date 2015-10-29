using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public Player player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
