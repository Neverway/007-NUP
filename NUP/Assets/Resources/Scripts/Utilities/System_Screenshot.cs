//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
//
// Purpose:
//			Take a screenshot with the current camera resolution and save to
//			the documents path
// Applied to:
//			The persistent system manager
// Notes:
//			Code expertly copied and pasted from
//			https://gamedevplanet.com/how-to-take-a-screenshot-from-within-your-game-in-unity/
//
//=============================================================================

using UnityEngine;
using System;
using System.IO;

public class System_Screenshot : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    [SerializeField] private KeyCode screenshotKey;


    //=-----------------=
    // Private Variables
    //=-----------------=
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Update()
    {
	    if (Input.GetKeyDown(screenshotKey))
	    {
		    SaveScreenshotToDocuments();
	    }
    }
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private static void SaveScreenshotToDocuments()
    {
	    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	    var screenshotPath = Path.Combine(documentsPath, Application.productName, "Screenshots");

	    var screenshotDirectory = Directory.CreateDirectory(screenshotPath);
	    print(screenshotPath);
	    var timeNow = DateTime.Now.ToString("dd-MMMM-yyyy HHmmss");
	    ScreenCapture.CaptureScreenshot(Path.Combine(screenshotDirectory.FullName, "DA-"+timeNow+".png"));
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}

