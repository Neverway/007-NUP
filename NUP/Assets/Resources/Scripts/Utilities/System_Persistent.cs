//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
//
// Purpose:
//          Keep the game object this script is attached to from being
//          destroyed when changing scenes
// Applied to:
//			The root of any game object that should persist across scenes
// Notes: 
//
//=============================================================================

using UnityEngine;

public class System_Persistent : MonoBehaviour
{
	//=-----------------=
	// Public variables
	//=-----------------=
	

	//=-----------------=
	// Private variables
	//=-----------------=
	
	
	//=-----------------=
	// Reference variables
	//=-----------------=
	private static System_Persistent instance;


	//=-----------------=
	// Mono Functions
	//=-----------------=
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}
    
    
	//=-----------------=
	// Internal Functions
	//=-----------------=


	//=-----------------=
	// External Functions
	//=-----------------=
}
