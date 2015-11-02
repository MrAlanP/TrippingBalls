using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public GameObject mainMenu;
	public GameObject optionsMenu;
	public GameObject controlsMenu;
	public GameObject PCControlsMenu;
	public GameObject ControllerControlsMenu;
	public AudioSource GameMusic;
	public float MusicVolume;
	public Slider MusicVolumeSlider;
	public GameObject game;

	public GameObject menu;

	GameObject currentMenu; //The current menu we have open


	enum MenuState{
		Main,
		Options,
		Controls,
		PCControls,
		ControllerControls
	}


	//Awake gets called when the class is instantiated
	void Awake () {
		game.SetActive (false);
		//MusicVolume = GlobalAudioController.Instance.MusicVolume;
		GameMusic.volume = MusicVolumeSlider.value;
		GameMusic.Play ();
		optionsMenu.SetActive (false);
		controlsMenu.SetActive (false);
		ControllerControlsMenu.SetActive (false);
		PCControlsMenu.SetActive (false);

		SetMenuState (MenuState.Main);
	}
	
	// Update is called once per frame
	void Update () {
		MusicVolume = MusicVolumeSlider.value;
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
		case MenuState.Controls:
			currentMenu = controlsMenu;
			break;
		case MenuState.PCControls:
			currentMenu = PCControlsMenu;
			break;
		case MenuState.ControllerControls:
			currentMenu = ControllerControlsMenu;
			break;
		}

		//Set the new menu to active
		currentMenu.SetActive (true);
	}


	//Change scene to the game scene
	/*If you click on the StartButton gameobject in the heirarchy (Menu->MenuCanvas->MainMenu->StartButton) 
	  and scroll down to On Click() you can see where this is linked*/
	public void StartGame(){
		gameObject.SetActive (false);
		game.SetActive (true);
	}

	//Sets the main menu gameobject to inactive and the options gameobject to active
	public void OpenOptions(){
		SetMenuState (MenuState.Options);
	}

	public void CloseOptions(){
		SetMenuState (MenuState.Main);
	}

	public void OpenControls(){
		SetMenuState (MenuState.Controls);
	}

	public void CloseControls(){
		SetMenuState (MenuState.Main);
	}

	public void OpenPCControls(){
		SetMenuState (MenuState.PCControls);
	}
	
	public void ClosePCControls(){
		SetMenuState (MenuState.Controls);
	}

	public void OpenControllerControls(){
		SetMenuState (MenuState.ControllerControls);
	}
	
	public void CloseControllerControls(){
		SetMenuState (MenuState.Controls);
	}

	//Quit the game
	public void Quit(){
		Application.Quit (); //Function won't work in editor mode
	}

	public void SaveSettings(){
		GlobalAudioController.Instance.MusicVolume = MusicVolume;
	}
}
