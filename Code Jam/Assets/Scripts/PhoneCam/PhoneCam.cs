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
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0 )
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backcam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

            }
        }

        if (backcam == null)
        {
            Debug.Log("Unable to find back Camera");
            return;
        }

        backcam.Play();
        background.texture = backcam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable) return;
        
        float ratio = (float)backcam.width / (float)backcam.height;
        fit.aspectRatio = ratio;

        float scaleY = backcam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        //int orient = -backcam.videoRotationAngle;
        //background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);
    }

    
}
