using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHUD : MonoBehaviour {
	public GameObject bossHealth;
	public GameObject victory;

	// Use this for initialization
	void Awake () 
    {
		victory.SetActive (false);
		bossHealth.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
       
           
      

	
	}

	public void UpdateRubberBandsCount(int count){
//		rubberBandsCount.text = count.ToString();
       


	}

	public void ShowVictory(){
		victory.SetActive (true);
	}

	public void ShowBossHealth(){
		bossHealth.SetActive (true);
	}
}
