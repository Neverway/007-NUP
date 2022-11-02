//========== Neverway 2022 Project Script | Written by Unknown Dev ============
// 
// Purpose: 
//			Spawn the client's character on the server
// Applied to: 
//			The local system manager
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Network_Client_CharcterInstantiator : MonoBehaviour
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
    private void Start()
    {
	
    }

    private void Update()
    {
	
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void SpawnLocalPlayer()
    {
	    // Look through the clients on the server
	    foreach (var client in FindObjectsOfType<Network_Client>())
	    {
		    // If we're that client
		    if (client.OwnerClientId == NetworkManager.Singleton.LocalClientId)
		    {
			    // If we're a client
			    if (NetworkManager.Singleton.IsHost)
			    {
				    print("HOST SPAWN");
				    client.InstantiatePlayerServer(NetworkManager.Singleton.LocalClientId);
			    }
			    else
			    {
				    print("CLIENT SPAWN");
				    client.InstantiatePlayerServerRpc(NetworkManager.Singleton.LocalClientId);
			    }
		    }
	    }
    }
}

