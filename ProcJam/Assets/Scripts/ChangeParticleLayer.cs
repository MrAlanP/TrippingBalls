using UnityEngine;
using System.Collections;

public class ChangeParticleLayer : MonoBehaviour {
	public string sortingLayer = "Foreground";
	// Use this for initialization
	void Awake () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayer; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
