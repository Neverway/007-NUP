//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//          Asynchronously load a target scene and unload the current scene
// Applied to: 
//			The persistent system manager
// Notes: 
//          _DelayBeforeLoad is used for a buffer in-case there is some transition
//          animation that should play before switching to the loading screen
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System_SceneManager : MonoBehaviour
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

    
	//=-----------------=
	// Mono Functions
	//=-----------------=
	private static IEnumerator Load(string _sceneName, float _delayBeforeLoad)
	{
		// Confirm that the target scene is not the loading scene
		if (_sceneName.ToLower() == "loading")
		{
			// The target scene is the loading scene, so throw an error and return to the title screen 
			Debug.LogWarning("Attempted to load the loading screen... What the heck?!");
			SceneManager.LoadScene(0);
		}
		else
		{
			// The target scene was not the loading scene, so load the target scene
			PlayerPrefs.SetString("LoadingSceneID", _sceneName);
			yield return new WaitForSeconds(_delayBeforeLoad);
			SceneManager.LoadScene("Loading"); // Switch to loading screen
		}
	}
    
    
	//=-----------------=
	// Internal Functions
	//=-----------------=
    
    
	//=-----------------=
	// External Functions
	//=-----------------=
	public void LoadScene(string _sceneName, float _delayBeforeLoad)
	{
		StartCoroutine(Load(_sceneName, _delayBeforeLoad));
	}
}
