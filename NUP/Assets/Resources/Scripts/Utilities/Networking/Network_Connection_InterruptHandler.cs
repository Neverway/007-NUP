//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Properly remove a client from a server that's shutdown incorrectly
// Applied to: 
//			The system manager
// Notes: 
//			CURRENTLY IN PROGRESS OF WRITING! DO NOT IMPLEMENT IN RELEASE!
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


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    if (!networkManager) networkManager = FindObjectOfType<NetworkManager>();
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
	    print("HC True:" + hasConnectedToServer);
	    //print("CL True: " + !networkManager.IsConnectedClient);
	    // Client connection check
	    if (!networkManager.IsConnectedClient && hasConnectedToServer)
	    {
		    if (FindObjectsOfType<Network_Client>().Length == 0) print("TIME TO LEAVE!");
	    }
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}

