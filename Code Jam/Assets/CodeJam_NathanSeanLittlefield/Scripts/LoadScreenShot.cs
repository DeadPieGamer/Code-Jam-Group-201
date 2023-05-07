using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenShot : MonoBehaviour
{
    private PhoneCam PhoneCam;
    private Texture2D newTexture;
    [SerializeField] private GameObject bg;
    [SerializeField] private RawImage Image;
    [SerializeField] private string input;

    private float wait_time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if(bg != null) PhoneCam = bg.GetComponent<PhoneCam>();//Since this script are being used in 2 seperate scenes
                                                              //and only the scanning scene has the bg element
                                                              //the phonecam only get's assigned when there's a bg.
        Rerender(input);
    }

    /// <summary>
    /// Render the screenshot file as a texture
    /// </summary>
    /// <param name="saveName"></param>
    public void Rerender(string saveName)
    {
        var filePath = Application.dataPath;//for real builds this must be switch out
                                            //for Application.PersistantDataPath since
                                            //in the real build dataPath is inaccessible
        string folderName = "/CodeJam_NathanSeanLittlefield/SavedScreenShots/";

        if (PhoneCam)
        {
            StartCoroutine(wait(saveName));
        }
        else
        {
            if (File.Exists(filePath + folderName + saveName + ".png"))//if the file in the provided path exist
            {
                byte[] data = File.ReadAllBytes(filePath + folderName + saveName + ".png");//store the byte data of the image in the specific filepath
                newTexture = new Texture2D(1, 1);//as the new texture will resize to the image the size here does not matter but it cannot be null
                ImageConversion.LoadImage(newTexture, data);//Convery the byte[] data into a texture

                Image.texture = newTexture;//set the new texture
            }
        }
    }

    private IEnumerator wait(string saveName)
    {
        var filePath = Application.dataPath;
        string folderName = "/CodeJam_NathanSeanLittlefield/SavedScreenShots/";

        yield return new WaitForSeconds(wait_time);

        if (PhoneCam.backcam != null) PhoneCam.backcam.Stop();//stop playing the camera input on the BG

        //the section below works the same as the rerender but
        //this is for phonecam and it's in a Ienumerator
        //because I wanted to add a wait time before the bg get's replaced
        if (File.Exists(filePath + folderName + saveName + ".png"))
        {
            byte[] data = File.ReadAllBytes(filePath + folderName + saveName + ".png");
            newTexture = new Texture2D(1, 1);
            ImageConversion.LoadImage(newTexture, data);

            PhoneCam.background.texture = newTexture;
        }
    }
}
