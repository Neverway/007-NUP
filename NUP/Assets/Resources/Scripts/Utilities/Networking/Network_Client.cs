//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Store network player data like their username & what level they're on
// Applied to: 
//			The root of the network instantiated client entry prefab
// Notes: 
//			
//
//=============================================================================

using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network_Client : NetworkBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    // Used by external scripts (like Network_Client_Lister) to reference the players on the server by name
    public string username;
    // Used by external scripts (like Network_Connection_Connector) to reference what scene each player is on
    public string currentScene;


    //=-----------------=
    // Private Variables
    //=-----------------=
    // Network var of username
    public NetworkVariable<FixedString32Bytes> _username 
	    = new NetworkVariable<FixedString32Bytes>(
		    "MissingData",
		    NetworkVariableReadPermission.Everyone,
		    NetworkVariableWritePermission.Owner);
    // Network var of currentScene
    public NetworkVariable<FixedString128Bytes> _currentScene
	    = new NetworkVariable<FixedString128Bytes>(
		    "0",
		    NetworkVariableReadPermission.Everyone,
		    NetworkVariableWritePermission.Owner);
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject localPlayer;


    //=-----------------=
    // Mono Functions
    //=-----------------=

    private void Update()
    {
	    UpdateLocalUsername();
	    UpdateLocalScene();
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateLocalUsername()
    {
	    // If the local client owns this object, pull their username from player prefs
	    // Assign net value
	    if (IsOwner) _username.Value = PlayerPrefs.GetString("NetClientUsername");
	    // Assign the network value to a local value to make it easier to reference
	    // Parse net value for easier reference in scripts
	    username = _username.Value.ToString();
    }
    
    private void UpdateLocalScene()
    {
	    // Assign net value
	    if (IsOwner) _currentScene.Value = SceneManager.GetActiveScene().name;
	    // Parse net value for easier reference in scripts
	    currentScene = _currentScene.Value.ToString();
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    // Spawn the local player on the server side
    public void InstantiatePlayerServer(ulong requesterID)
    {
	    // Create a reference to the instantiated player object
	    var instantiatedPlayer = Instantiate(playerPrefab);
	    // Set the player object to destroy on scene change
	    instantiatedPlayer.GetComponent<NetworkObject>().Spawn();
	    // Set the instantiated player's owner as the client that sent the request 
	    instantiatedPlayer.GetComponent<NetworkObject>().ChangeOwnership(requesterID);
	    // Assign the local player on the server side (Need to find a way to do this on client side as well)
	    localPlayer = instantiatedPlayer;
    }
    [ServerRpc(RequireOwnership = false)]
    public void InstantiatePlayerServerRpc(ulong requesterID)
    {
	    // Create a reference to the instantiated player object
	    var instantiatedPlayer = Instantiate(playerPrefab);
	    // Set the player object to destroy on scene change
	    instantiatedPlayer.GetComponent<NetworkObject>().Spawn();
	    // Set the instantiated player's owner as the client that sent the request 
	    instantiatedPlayer.GetComponent<NetworkObject>().ChangeOwnership(requesterID);
	    // Assign the local player on the server side (Need to find a way to do this on client side as well)
	    localPlayer = instantiatedPlayer;
    }
    /* Old code for spawning a player entity, needs to be redone so commenting it out for now while reworking rest of
     script
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
	*/
}

