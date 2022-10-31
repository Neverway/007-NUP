//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Set the target IP and port for hosting or joining a server
// Applied to: 
//			The local system manager on the network title scene
// Notes: 
//			UnityTransport, the component script that handles connections 
//			 (among other things), uses a data efficient variable type called 
//			 ushort. TryParse is used to turn our string of numbers into a
//           typeOf ushort
//=============================================================================

using System.Net;
using TMPro;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class Network_Connection_SetNetTarget : MonoBehaviour
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
    private UnityTransport transport;
    [Tooltip("The input field where the user types their hosting or connecting ip address")]
    [SerializeField] private TMP_InputField addressField;
    [Tooltip("The input field where the user types their hosting or connecting ip port")]
    [SerializeField] private TMP_InputField portField;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    if (!transport) transport = FindObjectOfType<UnityTransport>();
    }

    private void Update()
    {
	    UpdateTargetAddress();
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateTargetAddress()
    {
	    // If the player prefs for target address and port are not created, create them with the following default values
	    if (!PlayerPrefs.HasKey("NetTargetAddress")) PlayerPrefs.SetString("NetTargetAddress", "127.0.0.1");
	    if (!PlayerPrefs.HasKey("NetTargetPort")) PlayerPrefs.SetString("NetTargetPort", "25565");
	    
	    // Exit function if we can't change the address
	    // (null address/port field is quick way for me to check that it can't be changed)
	    if (!addressField) return;
	    // Updated placeholder text for address field
	    addressField.transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("NetTargetAddress");
	    // Assign value to transport
	    transport.ConnectionData.Address = PlayerPrefs.GetString("NetTargetAddress");
	    
	    if (!portField) return;
	    // Updated placeholder text for port field
	    portField.transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("NetTargetPort");
	    // Parse value for transport (transport.connectionData.Port expects a ushort instead of the raw string)
	    // It's kind of redundant to parse it again here since the player pref value should be valid, but whatever
	    ushort.TryParse(PlayerPrefs.GetString("NetTargetPort"), out var parsedPort);
	    // Assign value to transport
	    transport.ConnectionData.Port = parsedPort;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    [Tooltip("Set the target network address from TextMeshPro inputField")]
    public void NetworkSetAddress()
    {
	    // Check the value set in the input field
	    IPAddress.TryParse(addressField.text, out var parsedAddress);
	    // If the value is invalid, set it to a default value
	    if (parsedAddress == null) PlayerPrefs.SetString("NetTargetAddress", "127.0.0.1");
	    // Assign the value to the network transport
	    else PlayerPrefs.SetString("NetTargetAddress", parsedAddress.ToString());
	    // Clear field
	    addressField.text = "";
    }
    [Tooltip("Set the target network port from TextMeshPro inputField")]
    public void NetworkSetPort()
    {
	    // Check the value set in the input field
	    ushort.TryParse(portField.text, out var parsedPort);
	    // If the value is invalid, set it to a default value
	    if (parsedPort == 0) PlayerPrefs.SetString("NetTargetPort", "25565");
	    // Assign the value to the network transport
	    else PlayerPrefs.SetString("NetTargetPort", parsedPort.ToString());
	    // Clear field
	    portField.text = "";
    }
}

