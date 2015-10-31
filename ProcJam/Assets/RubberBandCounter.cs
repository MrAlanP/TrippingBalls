using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RubberBandCounter : MonoBehaviour {
	
	public GameObject[] rubberBands;
	

	public void UpdateRubberBandsCount(int count){

		for(int i = rubberBands.Length-1; i>=0; i--){
			if(i>=count){
				rubberBands[i].SetActive(false);
			}
			else{
				rubberBands[i].SetActive(true);
			}
			
		}
	}
}
