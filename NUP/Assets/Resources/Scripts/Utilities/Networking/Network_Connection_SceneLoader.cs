//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Load the appropriate starting scene when connecting to a server
// Applied to: 
//			The local system manager on the network title scene
// Notes: 
//			
//
//=============================================================================

using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network_Connection_SceneLoader : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    [Tooltip ("The string ID of the scene that the server should load when starting a new server")]
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
	    // Find reference objects
	    sceneManager = FindObjectOfType<System_SceneManager>();
	    networkManager = FindObjectOfType<NetworkManager>();
    }

    private IEnumerator WaitForClientInstantiation()
    {
	    yield return new WaitForSeconds(0.5f);
	    ClientConnectToScene(GetCurrentServerScene());
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=S
    // Load a specified starting scene
    private void HostConnectToScene(string startingScene)
    {
	    sceneManager.LoadScene(startingScene, 0f);
    }
    
    // Load the scene that the server is currently on
    private void ClientConnectToScene(string serverScene)
    {
	    sceneManager.LoadScene(serverScene, 0f);
    }
    
    // Get which scene the server is currently on
    private string GetCurrentServerScene()
    {
	    // Set the default return value to the first scene in the build list
	    var value = hostStartingScene;
	    // Look through all players on the server
	    foreach (var client in FindObjectsOfType<Network_Client>())
	    {
		    // If the player is the host, set the return value to what scene they are on
		    if (client.OwnerClientId == NetworkManager.ServerClientId) value = client.currentScene;
	    }
	    // Return the set value
	    return value;
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
	    else if (networkManager.IsClient) StartCoroutine(WaitForClientInstantiation());
    }
}

