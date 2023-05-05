using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Snap_Cast : MonoBehaviour
    
{
    private static Snap_Cast _instance;

    private Camera _cam;
    private bool _takeScreenSnap = false;
    public bool _saveFile = false;
    public GameObject _myDisplay;

    public string Filename;

    public void Awake()
    {
        _myDisplay = GameObject.FindGameObjectWithTag("Writeable");
    }

    public static void TakeSnap_Static(int width, int height)
    {
        _instance.takeSnap(width, height); 
    }

    private void Start()
    {
        _instance = this;
        _cam = this.gameObject.GetComponent<Camera>();
    }
    private void takeSnap(int width, int height)
    {
        _cam.targetTexture =
            RenderTexture.GetTemporary(width, height, 16);//16 WHY????
        _takeScreenSnap = true;
    }
    private void OnPostRender()
    {
        if (_takeScreenSnap == true)
        {
            _takeScreenSnap = false;
            RenderTexture rendTex = _cam.targetTexture;

            //retrive temporary render as texture
            Texture2D renderResult = new Texture2D(rendTex.width, rendTex.height, TextureFormat.ARGB32, false);

            //gather pixels from texture
            Rect rect = new Rect(0, 0, rendTex.width, rendTex.width);
            renderResult.ReadPixels(rect, 0, 0);

            //apply pixels
            renderResult.Apply();

            //apply texture
            _myDisplay.GetComponent<Renderer>().material.mainTexture = renderResult;
            DontDestroyOnLoad(this._myDisplay);

            //save to file
            if (_saveFile==true)
            {
                byte[] bytes = renderResult.EncodeToPNG();
                string Path = Application.persistentDataPath + "/" + Filename + ".png";
                File.WriteAllBytes(Path, bytes);
            }

            //reset camera
            RenderTexture.ReleaseTemporary(rendTex);
            _cam.targetTexture = null;
        }
    }
}
