//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Start connections for server as either Server, Host, or Client
// Applied to: 
//			The local system manager
// Notes:
//			Line 84 doesn't seem like it's handled correctly, though the 
//			 OnConnected() function still seems to be firing
//=============================================================================

using System.Collections;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Network_Connection_Manager : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=


    //=-----------------=
    // Private Variables
    //=-----------------=
    private bool attemptingConnection;
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    private NetworkManager networkManager;
    private System_SceneManager sceneManager;
    private Network_Connection_InterruptHandler interruptHandler;

    [SerializeField] private string titleScene = "0";

    [SerializeField] private UnityEvent OnConnecting;
    [SerializeField] private UnityEvent OnConnectionFailed;
    [SerializeField] private UnityEvent OnInvalidAddress;
    [SerializeField] private UnityEvent OnConnected;
    [SerializeField] private UnityEvent OnDedicatedServerConnected;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    // Client connection timed out
    private IEnumerator ConnectionTimeout()
    {
	    // Start a five second countdown
	    yield return new WaitForSeconds(5);
	    // If connection to server has been established, exit function
	    if (!attemptingConnection) yield break;
	    // Stop attempting to connect
	    attemptingConnection = false;
	    // Shutdown the connection attempt
	    NetworkDisconnect();
	    // Fire on connection failed
	    OnConnectionFailed.Invoke();
    }
    
    private void Update()
    {
	    if (!networkManager) networkManager = FindObjectOfType<NetworkManager>();
	    if (!sceneManager) sceneManager = FindObjectOfType<System_SceneManager>();
	    if (!interruptHandler) interruptHandler = FindObjectOfType<Network_Connection_InterruptHandler>();
	    
	    ClientJoiningConnectionCheck();
    }
    

    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    private void ClientJoiningConnectionCheck()
    {
	    // Client connection check
	    if (!attemptingConnection || !networkManager.IsConnectedClient) return;
	    // Set that we should be connected to the server (used to check for connection interrupts)
	    interruptHandler.hasConnectedToServer = true;
	    // Set this to false so the connectionTimeout function will stop
	    // Stop attempting to connect
	    attemptingConnection = false;
	    // Fire on connected for client (User sets if this sends them to a game, or shows a lobby, etc.)
	    OnConnected.Invoke();
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    //=-----------------=
    // CONNECT
    //=-----------------=
    [Tooltip("Connect to target address and port as dedicatedServer")]
    public void NetworkConnectServer()
    {
	    networkManager.StartServer();
	    // Fire on dedicated server started
	    OnDedicatedServerConnected.Invoke();
    }
    [Tooltip("Connect to target address and port as host")]
    public void NetworkConnectHost()
    {
	    // Start host if the address and port is valid
	    // Ports share the same limits as a ushort, so parsing the target port as ushort will return only valid ports
	    if (IPAddress.TryParse(PlayerPrefs.GetString("NetTargetAddress"), out var address) && ushort.TryParse(PlayerPrefs.GetString("NetTargetPort"), out var port))
	    {
		    // Set that we should be connected to the server (used to check for connection interrupts)
		    interruptHandler.hasConnectedToServer = true;
			networkManager.StartHost();
	    }
	    // Show error message if address is not valid
	    else OnInvalidAddress.Invoke();
	    // Fire on connected for client (User sets if this sends them to a game, or shows a lobby, etc.)
	    OnConnected.Invoke();
    }
    [Tooltip("Connect to target address and port as client")]
    public void NetworkConnectClient()
    {
	    // Start host if the address and port is valid
	    // Ports share the same limits as a ushort, so parsing the target port as ushort will return only valid ports
	    if (IPAddress.TryParse(PlayerPrefs.GetString("NetTargetAddress"), out var address) && ushort.TryParse(PlayerPrefs.GetString("NetTargetPort"), out var port))
	    {
		    networkManager.StartClient();
		    // Fire on connecting (show the 'connecting...' message)
		    OnConnecting.Invoke();
		    // Start connection timeout timer
		    StartCoroutine(ConnectionTimeout());
		    // Check for successful connection in update
		    attemptingConnection = true;
	    }
	    // Show error message if address is not valid
	    else OnInvalidAddress.Invoke();
    }
    
    //=-----------------=
    // DISCONNECT
    //=-----------------=
    [Tooltip("Shutdown the server")]
    public void NetworkDisconnect()
    {
	    // Stop the connection check (in case it was running)
	    attemptingConnection = false;
	    // Set that we should not be connected to the server (used to check for connection interrupts)
	    interruptHandler.hasConnectedToServer = false;
	    // Disconnect the server
	    networkManager.Shutdown();
	    // Load the title scene if it's not already loaded
	    if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(titleScene))
		    sceneManager.LoadScene(titleScene, 0);
    }

	//=-----------------=
	// SHORT CUTS
	//=-----------------=
	/* I don't think this function is in use anymore, commenting it out for now
	[Tooltip("Find the local client object")]
	public Network_Client NetworkLocalClient()
	{
		// Set default return to null
		Network_Client localClient = null;

		// Look through all Network_Client objects in the scene
		foreach (var client in FindObjectsOfType<Network_Client>())
		{
			// Set localClient to client if local client is owner
			if (client.IsOwner) localClient = client;
		}

		// Return result
		return localClient;
	}*/
}

