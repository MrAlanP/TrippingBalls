using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHUD : MonoBehaviour {
	public GameObject bossHealth;

	// Use this for initialization
	void Awake () 
    {
		bossHealth.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
       
           
      

	
	}

	public void UpdateRubberBandsCount(int count){
//		rubberBandsCount.text = count.ToString();
       


	}

	public void ShowBossHealth(){
		bossHealth.SetActive (true);
	}
}
