using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScreenshot : MonoBehaviour
{
   
    public void TakeScreenShot(string saveName)
    {
        var filePath = Application.dataPath;//for real builds this must be switch out for
                                            //Application.PersistantDataPath since in the real build dataPath is inaccessible 
        string folderName = "/CodeJam_NathanSeanLittlefield/SavedScreenShots/";
        
        if (!Directory.Exists(filePath+folderName))//create the folder if it does not exist
        {
            Directory.CreateDirectory(filePath + folderName);
        }
        
        ScreenCapture.CaptureScreenshot(filePath + folderName + saveName + ".png");//save the screenshot to a specific location
                                                                                   //in the asset folder 
    }
}
