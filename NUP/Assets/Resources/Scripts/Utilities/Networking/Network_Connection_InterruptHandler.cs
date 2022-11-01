//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Properly remove a client from a server that's shutdown incorrectly
// Applied to: 
//			The system manager
// Notes: 
//
//=============================================================================

using Unity.Netcode;
using UnityEngine;

public class Network_Connection_InterruptHandler : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    // True if we should be connected to the server (set by local Network_Connection_Manager)
    public bool hasConnectedToServer;


    //=-----------------=
    // Private Variables
    //=-----------------=
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    private NetworkManager networkManager;
    private System_SceneManager sceneManager;
    private Network_Connection_Manager connectionManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    if (!networkManager) networkManager = FindObjectOfType<NetworkManager>();
	    if (!sceneManager) sceneManager = FindObjectOfType<System_SceneManager>();
	    if (!connectionManager) connectionManager = FindObjectOfType<Network_Connection_Manager>();
    }

    private void Update()
    {
	    ConnectionInterruptCheck();
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void ConnectionInterruptCheck()
    {
	    // If not connected to server & should be connected to server & no clients are found
	    if (!networkManager.IsConnectedClient && hasConnectedToServer && FindObjectsOfType<Network_Client>().Length == 0)
	    {
		    // Properly close the connection (also sends user back to the title scene)
		    connectionManager.NetworkDisconnect();
	    }
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}

