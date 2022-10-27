//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Start connections for server as either Server, Host, or Client, 
//			 and allow the target address and port to be modified
// Applied to: 
//			The local system manager
// Notes:
//			UnityTransport, the component script that handles connections 
//			 (among other things), uses a data efficient variable type called 
//			 ushort. TryParse is used to turn our string of numbers into a
//           typeOf ushort
//=============================================================================

using System;
using System.Collections;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Network_Connection_Manager : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    public string targetAddress;
    public string targetPort;


    //=-----------------=
    // Private Variables
    //=-----------------=
    private bool attemptingConnection;
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    private UnityTransport transport;
    private NetworkManager networkManager;
    [Header("Title References")]
    [Tooltip("The input field where the user types their hosting or connecting ip address")]
    [SerializeField] private TMP_InputField addressField;
    [Tooltip("The input field where the user types their hosting or connecting ip port")]
    [SerializeField] private TMP_InputField portField;

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
	    if (!transport) transport = FindObjectOfType<UnityTransport>();
	    if (!networkManager) networkManager = FindObjectOfType<NetworkManager>();
	    
	    UpdateTargetAddress();

	    // Client connection check
	    if (attemptingConnection && networkManager.IsConnectedClient)
	    {
		    // Set this to false so the connectionTimeout function will stop
		    // Stop attempting to connect
		    attemptingConnection = false;
		    // Fire on connected
		    OnConnected.Invoke();
	    }
    }
    

    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateTargetAddress()
    {
	    // Exit function if we can't change the address
	    // (null address/port field is quick way for me to check that it can't be changed)
	    if (!addressField) return;
	    // Updated placeholder text for address field
	    addressField.transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("NetTargetAddress");
	    
	    if (!portField) return;
	    // Updated placeholder text for port field
	    portField.transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("NetTargetPort");
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
	    if (IPAddress.TryParse(targetAddress, out var test) && ushort.TryParse(targetPort, out var tests))
		    networkManager.StartHost();
	    // Show error message if address is not valid
	    else OnInvalidAddress.Invoke();
    }
    [Tooltip("Connect to target address and port as client")]
    public void NetworkConnectClient()
    {
	    // Start host if the address and port is valid
	    // Ports share the same limits as a ushort, so parsing the target port as ushort will return only valid ports
	    if (IPAddress.TryParse(targetAddress, out var test) && ushort.TryParse(targetPort, out var tests))
	    {
		    networkManager.StartClient();
		    // Fire on connecting
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
	    // Stop the connection check
	    attemptingConnection = false;
	    // Disconnect the server
	    networkManager.Shutdown();
    }
    
    //=-----------------=
    // NET TARGET ADDRESS
    //=-----------------=
    [Tooltip("Set the target network address from TextMeshPro inputField")]
    public void NetworkSetAddress()
    {
	    // Check the value set in the input field
	    IPAddress.TryParse(addressField.text, out var parsedAddress);
	    // If the value is invalid, set it to a default value
	    if (parsedAddress == null)
	    {
		    targetAddress = "127.0.0.1";
		    // Assign the value to the player prefs
		    PlayerPrefs.SetString("NetTargetAddress", "127.0.0.1");
	    }
	    // Assign the value to the network transport
	    else
	    {
		    transport.ConnectionData.Address = parsedAddress.ToString();
		    // Assign the value to the player prefs
		    PlayerPrefs.SetString("NetTargetAddress", parsedAddress.ToString());
	    }
	    // Clear field
	    addressField.text = "";
    }
    [Tooltip("Set the target network port from TextMeshPro inputField")]
    public void NetworkSetPort()
    {
	    // Check the value set in the input field
	    ushort.TryParse(portField.text, out var parsedPort);
	    // If the value is invalid, set it to a default value
	    if (parsedPort == 0)
	    {
		    targetPort = "25565";
		    // Assign the value to the player prefs
		    PlayerPrefs.SetString("NetTargetPort", "25565");
	    }
	    // Assign the value to the network transport
	    else
	    {
		    transport.ConnectionData.Port = parsedPort;
		    // Assign the value to the player prefs
		    PlayerPrefs.SetString("NetTargetPort", parsedPort.ToString());
	    }
	    // Clear field
	    portField.text = "";
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

