using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject mainMenu;
	public GameObject optionsMenu;

	GameObject currentMenu; //The current menu we have open


	enum MenuState{
		Main,
		Options
	}


	//Awake gets called when the class is instantiated
	void Awake () {
		optionsMenu.SetActive (false);

		SetMenuState (MenuState.Main);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetMenuState(MenuState newState){

		//If the currentmenu has been assigned, hide it and reassign
		if (currentMenu != null) {
			currentMenu.SetActive(false);
		}

		switch (newState) {
		case MenuState.Main:
			currentMenu = mainMenu;
			break;
		case MenuState.Options:
			currentMenu = optionsMenu;
			break;
		}

		//Set the new menu to active
		currentMenu.SetActive (true);
	}


	//Change scene to the game scene
	/*If you click on the StartButton gameobject in the heirarchy (Menu->MenuCanvas->MainMenu->StartButton) 
	  and scroll down to On Click() you can see where this is linked*/
	public void StartGame(){
		Application.LoadLevel ("Game");
	}

	//Sets the main menu gameobject to inactive and the options gameobject to active
	public void OpenOptions(){
		SetMenuState (MenuState.Options);
	}

	public void CloseOptions(){
		SetMenuState (MenuState.Main);
	}

	//Quit the game
	public void Quit(){
		Application.Quit (); //Function won't work in editor mode
	}
}
