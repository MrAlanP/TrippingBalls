using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
    GameObject[] bandList;
    public GameObject player;
	public Text rubberBandsCount;
	// Use this for initialization
	void Start () 
    {
        //ArrayList bandslist = new ArrayList();
       bandList = GameObject.FindGameObjectsWithTag("bandTower");
	}
	
	// Update is called once per frame
	void Update () 
    {
        
       
           
      

	
	}

	public void UpdateRubberBandsCount(int count){
		rubberBandsCount.text = count.ToString();
        for (int i=0;i>count;i++)
        {
            bandList[i].SetActive(true);
                //GetComponent<SpriteRenderer>().enabled = true;
        }
        if (bandList.Length>count)
        {
            bandList[bandList.Length - count].SetActive(false);
        }


	}
}
