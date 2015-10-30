using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RubberBandCounter : MonoBehaviour {
	
	public GameObject[] rubberBands;
	GameObject player;
	int rubberBandsCount;
	int prevRubberBandsCount;
	Player playerScript;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");

		playerScript = player.GetComponent<Player>();

		rubberBandsCount = playerScript.rubberBands.Capacity;
	
		rubberBands = GameObject.FindGameObjectsWithTag("bandTower");

		for(int i = 0; i < 9; i++){

			rubberBands[i].SetActive(true);

		}




	}
	
	// Update is called once per frame
	void Update () {


		//rubberBandsCount = playerScript.rubberBands.Count;

		//Debug.Log ("PREVIOUS RUBBERBANDS: " + prevRubberBandsCount);
		//Debug.Log ("CURRENT RUBBERBANDS: " + rubberBandsCount);

		for(int i = 0; i < 10; i++)
		{
			rubberBands[i].SetActive(false);
		}

		for(int i = 0; i < rubberBandsCount; i++)
		{
			rubberBands[i].SetActive(true);
		}


//		if(prevRubberBandsCount > rubberBandsCount){ //if theres less than there was
//
//			for(int i = 0; i < 10 - rubberBandsCount ; i++){ 
//				//Debug.Log(rubberBandsCount);
//				rubberBands[i].SetActive(false);
//			}
//		}
//		else if (prevRubberBandsCount < rubberBandsCount){ //if theres more than there was
//
//			for(int i = 0; i < rubberBandsCount; i++){
//				
//				rubberBands[i].SetActive(true);
//			}
//		}
//
//		prevRubberBandsCount = rubberBandsCount;
	
	}

	public void UpdateRubberBandsCount(int count){

		rubberBandsCount = count;

	}
}
