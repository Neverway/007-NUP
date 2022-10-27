//========== Neverway 2022 Project Script | Written by Unknown Dev ============
// 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Network_Connection_Messages : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    [SerializeField] private string connectingMessage;
    [SerializeField] private string connectionFailedMessage;
    [SerializeField] private string invalidAddressMessage;


    //=-----------------=
    // Private Variables
    //=-----------------=
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    private Network_Connection_Manager connectionManager;
    [SerializeField] private GameObject connectionMessageMenu;
    [SerializeField] private GameObject dedicatedServerMenu;
    [SerializeField] private TMP_Text connectionMessageText;
    [SerializeField] private TMP_Text dedicatedServerAddressText;
    [SerializeField] private GameObject[] connectingButtons;
    [SerializeField] private GameObject[] connectionFailedButtons;
    [SerializeField] private GameObject[] invalidAddressButtons;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    connectionManager = FindObjectOfType<Network_Connection_Manager>();
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void Connecting()
    {
	    // Show the connection message menu
	    connectionMessageMenu.SetActive(true);
	    // Show the connecting to server message
	    connectionMessageText.text = connectingMessage;
	    // Show the connecting to server buttons
	    foreach (var button in connectingButtons) button.SetActive(true);
	    // Hide the connection to server failed buttons
	    foreach (var button in connectionFailedButtons) button.SetActive(false);
	    // Hide the invalid address buttons
	    foreach (var button in invalidAddressButtons) button.SetActive(false);
    }
    public void Connected()
    {
	    
    }
    public void ConnectionFailed()
    {
	    // Show the connection message menu
	    connectionMessageMenu.SetActive(true);
	    // Show the connection to server failed message
	    connectionMessageText.text = connectionFailedMessage;
	    // Hide the connecting to server buttons
	    foreach (var button in connectingButtons) button.SetActive(false);
	    // Show the connection to server failed buttons
	    foreach (var button in connectionFailedButtons) button.SetActive(true);
	    // Hide the invalid address buttons
	    foreach (var button in invalidAddressButtons) button.SetActive(false);
    }
    public void InvalidAddress()
    {
	    // Show the connection message menu
	    connectionMessageMenu.SetActive(true);
	    // Show the invalid address message
	    connectionMessageText.text = invalidAddressMessage;
	    // Hide the connecting to server buttons
	    foreach (var button in connectingButtons) button.SetActive(false);
	    // Hide the connection to server failed buttons
	    foreach (var button in connectionFailedButtons) button.SetActive(false);
	    // Show the invalid address buttons
	    foreach (var button in invalidAddressButtons) button.SetActive(true);
    }
    public void DedicatedServerConnected()
    {
	    // Show the dedicated server menu
	    dedicatedServerMenu.SetActive(true);
	    // Show the dedicated server target address
	    dedicatedServerAddressText.text = PlayerPrefs.GetString("NetTargetAddress") + ":" + PlayerPrefs.GetString("NetTargetPort");
    }
}

