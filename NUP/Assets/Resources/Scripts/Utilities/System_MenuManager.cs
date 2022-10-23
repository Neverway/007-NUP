//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//          Fire events for pausing and un-pausing the game when a 'pausing menu'
//           is open
// Applied to: 
//          The root of a pausing menu
// Notes: 
//          A pausing menu is any UI menu that should halt the flow of gameplay
//           only one pausing menu can be active at a time
//          Unity events apparently don't display tooltip attributes, so I'm
//           changing them to headers for now
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;

public class System_MenuManager : MonoBehaviour
{
	//=-----------------=
	// Public Variables
	//=-----------------=
	[Tooltip("Reference and set this variable from all of your project's 'pausing menu' scripts")]
	public bool menuOpen; // Referenced by a project's Menu script to signify that a menu that should pause the game has been opened
	[Header("Execute your project's pause script's 'pause game' function here")]
	public UnityEvent OnPausingMenusOpen; // Fires once, when a pausing menu is opened
	[Header("Execute your project's pause script's 'unpause game' function here")]
	public UnityEvent OnPausingMenusClosed; // Fires once, when a pausing menu is closed


	//=-----------------=
	// Private Variables
	//=-----------------=
	private bool storedMenuOpenState;
    
    
	//=-----------------=
	// Reference Variables
	//=-----------------=


	//=-----------------=
	// Mono Functions
	//=-----------------=
	public void Update()
	{
		// Keep the events from firing more than once, by checking if the 'menu open state' has change from last loop
		if (storedMenuOpenState == menuOpen) return;
		// Update the stored menu open state
		storedMenuOpenState = menuOpen;
        
		// Execute appropriate functions
		if (menuOpen) { OnPausingMenusOpen.Invoke(); }
		else { OnPausingMenusClosed.Invoke(); }
	}

	//=-----------------=
	// Internal Functions
	//=-----------------=
    
    
	//=-----------------=
	// External Functions
	//=-----------------=
}

