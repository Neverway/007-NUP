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
using Unity.Netcode;
using UnityEngine;

public class Network_Client_Lister : MonoBehaviour
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
    public GameObject clientEntryPrefab;
    public Transform clientEntryParent;
    public List<GameObject> entries;
    public List<Network_Client> networkClients;


        //=-----------------=
    // Mono Functions
    //=-----------------=

    private void FixedUpdate()
    {
        networkClients.Clear();
        foreach (var entry in entries)
        {
            Destroy(entry);
        }
        entries.Clear();
        for (int i = 0; i < FindObjectsOfType<Network_Client>().Length; i++)
        {
            networkClients.Add(FindObjectsOfType<Network_Client>()[i]);
            var entry = Instantiate(clientEntryPrefab, clientEntryParent);
            entry.transform.GetChild(0).GetComponent<TMP_Text>().text = networkClients[i].username;
            entries.Add(entry);
        }
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}

