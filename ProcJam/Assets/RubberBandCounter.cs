using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RubberBandCounter : MonoBehaviour {

	List<GameObject> rubberBandsHud;
	public GameObject[] rubberBands;

	// Use this for initialization
	void Start () {

//		rubberBandsHud = new List<GameObject>();
//
//		rubberBandsHud.Add(GameObject.FindGameObjectsWithTag("bandTower"));
	
		rubberBands = GameObject.FindGameObjectsWithTag("bandTower");

		Debug.Log(rubberBands.Length);

	}
	
	// Update is called once per frame
	void Update () {

		for(int i = 0; i < 3; i++)
		{
			rubberBands[i].SetActive(false);
		}
	
	}
}
