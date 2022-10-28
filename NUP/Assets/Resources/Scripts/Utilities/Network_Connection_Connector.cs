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
    
    //=-----------------=
    // Internal Functions
    //=-----------------=S
    private void HostConnectToScene(string startingScene)
    {
	    sceneManager.LoadScene(startingScene, 0.2f);
    }
    
    private string GetCurrentServerScene()
    {
	    var value = "0";
	    // Look through all players on the server
	    foreach (var client in FindObjectsOfType<Network_Client>())
	    {
		    // If the player is the host, return the value of what scene they are on
		    if (client.gameObject.GetComponent<NetworkObject>().IsOwner) value = client.currentScene;
	    }
	    return value;
    }
    
    private void ClientConnectToScene(string serverScene)
    {
	    sceneManager.LoadScene(serverScene, 0.2f);
    }

    //=-----------------=
    // External Functions
    //=-----------------=
    // Called in the title scene by Network_Connection_Manager's OnConnected function
    public void ConnectToScene()
    {
	    // If the connecting player is the host, connect to the specified starting scene
	    if (networkManager.IsHost) HostConnectToScene(hostStartingScene);
	    // If the connecting player is a client, connect to the scene the host is currently on
	    else if (networkManager.IsClient) ClientConnectToScene(GetCurrentServerScene());
    }
}

