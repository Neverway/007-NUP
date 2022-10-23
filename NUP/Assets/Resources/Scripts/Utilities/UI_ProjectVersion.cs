//========== Neverway 2022 Project Script | Written by Unknown Dev ============
// 
// Purpose: 
//			Set a UI elements text to show the current project version
// Applied to: 
//			The root of a UI element that contains a TMP_Text or Text component
// Notes: 
//
//=============================================================================

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProjectVersion : MonoBehaviour
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
        var version = Application.version;
        if (GetComponent<TMP_Text>()) GetComponent<TMP_Text>().text = version;
        else if (GetComponent<Text>()) GetComponent<Text>().text = version;
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}

