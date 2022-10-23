//========== Neverway 2022 Project Script | Written by Unknown Dev ============
// 
// Purpose: 
//			If multiple cameras are in the scene, enable the audio listener
//			 for only the currently active camera
// Applied to: 
//			The system manager
// Notes: 
//
//=============================================================================

using UnityEngine;

public class System_Audio_SetActiveListener : MonoBehaviour
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


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Update()
    {
	    foreach (var camera in FindObjectsOfType<Camera>())
	    {
		    // If the camera is the current one, and it has an audio listener, enable the audio listener
		    if (camera == Camera.current)
		    {
			    if (camera.GetComponent<AudioListener>()) camera.GetComponent<AudioListener>().enabled = true;
		    }
		    // If the camera is not the current one, and it has an audio listener, disable the audio listener
		    else if (camera != Camera.current)
		    {
			    if (camera.GetComponent<AudioListener>()) camera.GetComponent<AudioListener>().enabled = false;
		    }
	    }
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}

