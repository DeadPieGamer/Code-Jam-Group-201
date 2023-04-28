using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//insprired by N3K EN-Device Camera
//https://youtu.be/c6NXkZWXHnc
public class PhoneCam : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backcam;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;//get all camerasdevieces in an array

        if (devices.Length == 0 )//if it cant find any cameras it does nothing & set camAvailable bool to false
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++) // for every devieces that was found if it's not a front camera it set the size and the camera to what it found
        {
            if (!devices[i].isFrontFacing)
            {
                backcam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

            }
        }

        if (backcam == null)//if there're no backcam it does nothing
        {
            Debug.Log("Unable to find back Camera");
            return;
        }

        backcam.Play();//display what the camera see on to the BG object
        background.texture = backcam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable) return;
        
        float ratio = (float)backcam.width / (float)backcam.height;//change the ratio of the BG object to the same size as the camera.
        fit.aspectRatio = ratio;

        float scaleY = backcam.videoVerticallyMirrored ? -1f : 1f; //if the camera is upsidedown it get's flipped
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        //int orient = -backcam.videoRotationAngle;
        //background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);
    }

    
}
