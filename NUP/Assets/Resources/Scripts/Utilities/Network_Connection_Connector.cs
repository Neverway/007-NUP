//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Load a multiplayer lobby, or connect a client to the server's
//			current scene
// Applied to: 
//			The local system manager on the title scene
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network_Connection_Connector : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    public string hostStartingScene;


    //=-----------------=
    // Private Variables
    //=-----------------=
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    private System_SceneManager sceneManager;
    private NetworkManager networkManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    sceneManager = FindObjectOfType<System_SceneManager>();
	    networkManager = FindObjectOfType<NetworkManager>();
    }

    private void Update()
    {
	
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void HostConnectToScene(string startingScene)
    {
	    sceneManager.LoadScene(startingScene, 0.2f);
    }
    
	/*
    private void GetCurrentServerScene()
    {
		    
    }
    
    private void ClientConnectToScene(string startingScene)
    {
	    sceneManager.LoadScene(startingScene, 0.2f);
    }
    */
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void ConnectToScene()
    {
	    if (networkManager.IsHost)
	    {
		    HostConnectToScene(hostStartingScene);
	    }
	    else if (networkManager.IsClient)
	    {
		    //ClientConnectToScene(GetCurrentServerScene());
	    }
    }
}

