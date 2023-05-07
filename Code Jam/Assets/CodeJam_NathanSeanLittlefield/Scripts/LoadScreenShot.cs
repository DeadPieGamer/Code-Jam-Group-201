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
        if(bg != null) PhoneCam = bg.GetComponent<PhoneCam>();
        Rerender(input);
    }

    public void Rerender(string saveName)
    {
        var filePath = Application.dataPath;
        string folderName = "/CodeJam_NathanSeanLittlefield/SavedScreenShots/";

        if (PhoneCam)
        {
            StartCoroutine(wait(saveName));
        }
        else
        {
            if (File.Exists(filePath + folderName + saveName + ".png"))
            {
                byte[] data = File.ReadAllBytes(filePath + folderName + saveName + ".png");
                newTexture = new Texture2D(1, 1);
                ImageConversion.LoadImage(newTexture, data);

                Image.texture = newTexture;
            }
        }
    }

    private IEnumerator wait(string saveName)
    {
        var filePath = Application.dataPath;
        string folderName = "/CodeJam_NathanSeanLittlefield/SavedScreenShots/";

        yield return new WaitForSeconds(wait_time);

        if (PhoneCam.backcam != null) PhoneCam.backcam.Stop();

        if (File.Exists(filePath + folderName + saveName + ".png"))
        {
            byte[] data = File.ReadAllBytes(filePath + folderName + saveName + ".png");
            newTexture = new Texture2D(1, 1);
            ImageConversion.LoadImage(newTexture, data);

            PhoneCam.background.texture = newTexture;
        }
    }
}
