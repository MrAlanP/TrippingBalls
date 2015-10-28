using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	public Text rubberBandsCount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateRubberBandsCount(int count){
		rubberBandsCount.text = count.ToString();
	}
}
