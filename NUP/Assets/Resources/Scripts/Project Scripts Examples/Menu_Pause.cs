//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using UnityEngine;

public class Menu_Pause : MonoBehaviour
{
	//=-----------------=
	// Public Variables
	//=-----------------=


	//=-----------------=
	// Private Variables
	//=-----------------=
	private bool active;
    
    
	//=-----------------=
	// Reference Variables
	//=-----------------=
	private System_MenuManager menuManager;


	//=-----------------=
	// Mono Functions
	//=-----------------=
	private void Start()
	{
		menuManager = FindObjectOfType<System_MenuManager>();
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape)) return;
		if (!active) OpenMenu();
		else CloseMenu();
	}
    
	//=-----------------=
	// Internal Functions
	//=-----------------=
	private void OpenMenu()
	{
		// Cancel if another pausing menu is open
		if (menuManager.menuOpen) return;
		// Show the pause menu
		transform.GetChild(0).gameObject.SetActive(true);
		// Set menu active
		active = true;
		// Inform the menu manager that a this pausing menu is open
		menuManager.menuOpen = true;
	}
    
	private void CloseMenu()
	{
		// Hide the pause menu
		transform.GetChild(0).gameObject.SetActive(false);
		// Set menu inactive
		active = false;
		// Inform the menu manager that a this pausing menu is no longer open
		menuManager.menuOpen = false;
	}
    
    
	//=-----------------=
	// External Functions
	//=-----------------=
}

