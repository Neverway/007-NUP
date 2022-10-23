//========== Neverway 2022 Project Script | Written by Unknown Dev ============
// 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Network_Client : NetworkBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    public string username;


    //=-----------------=
    // Private Variables
    //=-----------------=
    private NetworkVariable<FixedString32Bytes> _username 
	    = new NetworkVariable<FixedString32Bytes>(
		    "MissingData",
		    NetworkVariableReadPermission.Everyone,
		    NetworkVariableWritePermission.Owner);
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    [SerializeField] private GameObject playerPrefab;
    public GameObject localPlayer;


    //=-----------------=
    // Mono Functions
    //=-----------------=

    private void Update()
    {
	    UpdateLocalUsername();
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateLocalUsername()
    {
	    if (IsOwner) _username.Value = PlayerPrefs.GetString("NetClientUsername");
	    username = _username.Value.ToString();
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    // Spawn the local player on the server side
    [ServerRpc(RequireOwnership = false)]
    public void InstantiatePlayerServerRpc(ulong requesterID)
    {
	    // Create a reference to the instantiated player object
	    var instantiatedPlayer = Instantiate(playerPrefab);
	    // Set the player object to destroy on scene change
	    instantiatedPlayer.GetComponent<NetworkObject>().Spawn(true);
	    // Set the instantiated player's owner as the client that sent the request 
	    instantiatedPlayer.GetComponent<NetworkObject>().ChangeOwnership(requesterID);
	    // Assign the local player on the server side (Need to find a way to do this on client side as well)
	    localPlayer = instantiatedPlayer;
    }
    
    public void TryInstantiatePlayerSrpc(ulong requesterID)
    {
	    if (!IsOwner) return;
	    OldInstantiatePlayerServerRpc(requesterID);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void OldInstantiatePlayerServerRpc(ulong requesterID)
    {
	    //if (!IsOwner) return;
	    // Create a reference to the instantiated player object
	    var instantiatedPlayer = Instantiate(playerPrefab);
	    // Set the player object to destroy on scene change
	    instantiatedPlayer.GetComponent<NetworkObject>().Spawn(true);
	    // Set the instantiated player's owner as the client that sent the request 
	    instantiatedPlayer.GetComponent<NetworkObject>().ChangeOwnership(requesterID);
	    // Assign the local player on the server side (Need to find a way to do this on client side as well)
	    localPlayer = instantiatedPlayer;
    }

}

