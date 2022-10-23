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

using System.Collections;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

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
    [SerializeField] private TMP_InputField addressField;
    [SerializeField] private TMP_InputField portField;
    [SerializeField] private GameObject connectionMessageMenu;
    [SerializeField] private GameObject[] connectionMessageButtons;
    [SerializeField] private TMP_Text connectionMessage;


    //=-----------------=
    // Mono Functions
    //=-----------------=
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
	    // Hide the other button
	    connectionMessageButtons[0].SetActive(false);
	    // Show the close and reconnect buttons
	    connectionMessageButtons[1].SetActive(true);
	    connectionMessageButtons[2].SetActive(true);
	    // Show fail to connect message
	    connectionMessage.text = "Failed to connect!";
    }
    
    private void Update()
    {
	    if (!transport) transport = FindObjectOfType<UnityTransport>();
	    if (!networkManager) networkManager = FindObjectOfType<NetworkManager>();
	    
	    UpdateTargetAddress();

	    if (attemptingConnection)
	    {
			// Show connecting message
		    connectionMessage.text = "Establishing connection...";
		    
		    if (networkManager.IsConnectedClient)
		    {
			    // Set this to false so the connectionTimeout function will stop
			    // Stop attempting to connect
			    attemptingConnection = false;
			    // Show the cancel connection button
			    connectionMessageButtons[0].SetActive(true);
			    // Hide the other buttons
			    connectionMessageButtons[1].SetActive(false);
			    connectionMessageButtons[2].SetActive(false);
			    // Show connection established message
			    connectionMessage.text = "Connected to server!";
		    } 
	    }
    }
    

    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateTargetAddress()
    {
	    // Exit function if we can't change the address
	    // (null address field is quick way for me to check that it can't be changed)
	    if (!addressField) return;
	    
	    // Create starting NetTarget prefs
	    if (!PlayerPrefs.HasKey("NetTargetAddress")) PlayerPrefs.SetString("NetTargetAddress", "127.0.0.1");
	    if (!PlayerPrefs.HasKey("NetTargetPort")) PlayerPrefs.SetString("NetTargetPort", "25565");

	    // Set local vars to NetTarget prefs (makes code look neater)
	    targetAddress = PlayerPrefs.GetString("NetTargetAddress");
	    targetPort = PlayerPrefs.GetString("NetTargetPort");
	    
	    // Assign NetTarget to transport
	    transport.ConnectionData.Address = targetAddress;
	    ushort.TryParse(PlayerPrefs.GetString("NetTargetPort", "25565"), out var port);
	    transport.ConnectionData.Port = port;
	    
	    // Set input fields to show current address
	    addressField.transform.GetChild(0).GetComponent<TMP_Text>().text = targetAddress;
	    portField.transform.GetChild(0).GetComponent<TMP_Text>().text = targetPort;
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
    }
    [Tooltip("Connect to target address and port as host")]
    public void NetworkConnectHost()
    {
		networkManager.StartHost();
    }
    [Tooltip("Connect to target address and port as client")]
    public void NetworkConnectClient()
    {
	    networkManager.StartClient();
	    // Show connection message screen
	    connectionMessageMenu.SetActive(true);
	    // Show the cancel connection button
	    connectionMessageButtons[0].SetActive(true);
	    // Hide the other buttons
	    connectionMessageButtons[1].SetActive(false);
	    connectionMessageButtons[2].SetActive(false);
	    // Start connection timeout timer
	    StartCoroutine(ConnectionTimeout());
		// Check for successful connection in update
	    attemptingConnection = true;
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
	    // Fallback to localhost address if address is not specified
	    if (addressField.text == "") PlayerPrefs.SetString("NetTargetAddress", "127.0.0.1");
	    // Set network address to input field text
	    else PlayerPrefs.SetString("NetTargetAddress", addressField.text);
	    // Clear field
	    addressField.text = "";
    }
    [Tooltip("Set the target network port from TextMeshPro inputField")]
    public void NetworkSetPort()
    {
	    // Fallback to localhost port if port is not specified
	    if (portField.text == "") PlayerPrefs.SetString("NetTargetPort", "25565");
	    // Set network address to input field text
	    else PlayerPrefs.SetString("NetTargetPort", portField.text);
	    // Clear field
	    portField.text = "";
    }
    
    //=-----------------=
    // SHORT CUTS
    //=-----------------=
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
    }
}

