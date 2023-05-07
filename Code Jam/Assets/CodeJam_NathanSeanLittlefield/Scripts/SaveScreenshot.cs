using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScreenshot : MonoBehaviour
{
   
    public void TakeScreenShot(string saveName)
    {
        var filePath = Application.dataPath;
        string folderName = "/CodeJam_NathanSeanLittlefield/SavedScreenShots/";
        
        if (!Directory.Exists(filePath+folderName))
        {
            Directory.CreateDirectory(filePath + folderName);
        }
        
        ScreenCapture.CaptureScreenshot(filePath + folderName + saveName + ".png");
    }
}
