//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Display loading bar progress until a target scene is fully loaded
// Applied to: 
//			The local system manager on the loading scene
// Notes: 
//			The target "LoadingSceneID" is set by System_SceneLoader
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class System_Loading : MonoBehaviour
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
	[SerializeField] private Image loadingBar; // A reference to the loading bar


	//=-----------------=
	// Mono Functions
	//=-----------------=
	private void Start()
	{
		// Start Async scene load
		StartCoroutine(LoadAsyncOperation());
	}

	private IEnumerator LoadAsyncOperation()
	{
		// Create an async operation (Will automatically switch to target scene once it's finished loading)
		AsyncOperation gameLevel = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LoadingSceneID"));
	    
		// While it's still loading
		while (gameLevel.progress < 1)
		{
			// Set loading bar to reflect async progress
			loadingBar.fillAmount = gameLevel.progress;
			yield return new WaitForEndOfFrame();
		}
	}
    
    
	//=-----------------=
	// Internal Functions
	//=-----------------=
    
    
	//=-----------------=
	// External Functions
	//=-----------------=
}

