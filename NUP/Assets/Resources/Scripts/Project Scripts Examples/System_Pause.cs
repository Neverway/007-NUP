//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class System_Pause : MonoBehaviour
{
	//=-----------------=
	// Public Variables
	//=-----------------=


	//=-----------------=
	// Private Variables
	//=-----------------=
    
    
	//=-----------------=
	// Reference Variables
	//=-----------------=
	[SerializeField] private Text targetText;


	//=-----------------=
	// Mono Functions
	//=-----------------=
    
	//=-----------------=
	// Internal Functions
	//=-----------------=
    
    
	//=-----------------=
	// External Functions
	//=-----------------=
	public void PauseGame()
	{
		targetText.text = "GAME IS PAUSED";
	}
    
	public void UnpauseGame()
	{
		targetText.text = "GAME IS RUNNING";
	}
}

