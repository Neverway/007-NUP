//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//          List the players on the server in a UI element
// Applied to: 
//          The root of the player list UI element
// Notes: 
//
//=============================================================================

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Network_Client_Lister : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=


    //=-----------------=
    // Private Variables
    //=-----------------=
    [Tooltip("READ-ONLY needs to be public or errors are thrown when trying to clear list")]
    public List<GameObject> entries;
    [Tooltip("READ-ONLY needs to be public or errors are thrown when trying to clear list")]
    public List<Network_Client> networkClients;


    //=-----------------=
    // Reference Variables
    //=-----------------=
    [Tooltip("A UI element prefab to show player name (must have TMP_Text component as it's first child)")]
    [SerializeField] private GameObject clientEntryPrefab;
    [Tooltip("The parent UI element to create each clientEntryPrefab under")]
    [SerializeField] private Transform clientEntryParent;


    //=-----------------=
    // Mono Functions
    //=-----------------=

    private void Update()
    {
        // Erase the networkClients list
        networkClients.Clear();
        // Delete all of the UI entry prefabs
        foreach (var entry in entries) Destroy(entry);
        // Erase the entries list
        entries.Clear();
        
        // For every client entry
        for (int i = 0; i < FindObjectsOfType<Network_Client>().Length; i++)
        {
            // Add the client to the networkClients list
            networkClients.Add(FindObjectsOfType<Network_Client>()[i]);
            // Create the entry prefab for the UI
            var entry = Instantiate(clientEntryPrefab, clientEntryParent);
            // Set the clients name to the UI entry prefab that was just created
            entry.transform.GetChild(0).GetComponent<TMP_Text>().text = networkClients[i].username;
            // Add the UI entry prefab to the entries list
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

