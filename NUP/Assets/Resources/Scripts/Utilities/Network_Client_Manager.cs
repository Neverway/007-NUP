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

public class Network_Client_Manager : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    public string localClientUsername;
    

    //=-----------------=
    // Private Variables
    //=-----------------=
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    [SerializeField] private TMP_InputField usernameField;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	
    }

    private void Update()
    {
		UpdateLocalClientUsername();
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateLocalClientUsername()
    {
	    // Exit function if we can't change the address
	    // (null address field is quick way for me to check that it can't be changed)
	    if (!usernameField) return;
	    
	    // Create starting NetTarget prefs
	    if (!PlayerPrefs.HasKey("NetClientUsername")) PlayerPrefs.SetString("NetClientUsername", "NetPlayer");

	    // Set local vars to NetTarget prefs (makes code look neater)
	    localClientUsername = PlayerPrefs.GetString("NetClientUsername");
	    
	    // Assign username to local client
	    
	    // Set input fields to show current address
	    usernameField.transform.GetChild(0).GetComponent<TMP_Text>().text = localClientUsername;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    [Tooltip("Set the local client username")]
    public void NetworkSetLocalClientUsername()
    {
	    // Fallback to localhost address if address is not specified
	    if (usernameField.text == "") PlayerPrefs.SetString("NetClientUsername", "NetPlayer");
	    // Set network address to input field text
	    else PlayerPrefs.SetString("NetClientUsername", usernameField.text);
	    // Clear field
	    usernameField.text = "";
    }
}

