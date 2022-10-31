//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//			Set the player's network username via a TMP_InputField
// Applied to: 
//			The local system manager on the network title scene
// Notes: 
//			
//
//=============================================================================

using TMPro;
using UnityEngine;

public class Network_Client_SetLocalUsername : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    

    //=-----------------=
    // Private Variables
    //=-----------------=
    private string localClientUsername;
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=
    [SerializeField] private TMP_InputField usernameField;


    //=-----------------=
    // Mono Functions
    //=-----------------=

    private void Update()
    {
		UpdateLocalClientUsername();
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void UpdateLocalClientUsername()
    {
	    // Exit function if we can't change the username
	    // (null address field is quick way for me to check that it can't be changed)
	    if (!usernameField) return;
	    
	    // Create starting NetTarget prefs
	    if (!PlayerPrefs.HasKey("NetClientUsername")) PlayerPrefs.SetString("NetClientUsername", "NetPlayer");

	    // Set local vars to NetTarget prefs (makes code look neater)
	    localClientUsername = PlayerPrefs.GetString("NetClientUsername");
	    
	    // Set input field to show current name
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

