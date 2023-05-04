using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapInput_Cast : MonoBehaviour
{
    public Button SnapButton;

    private void Start()
    {
        SnapButton.onClick.AddListener(SnapShot);
    }
    void SnapShot()
    {
        {
            //Calling the funktion from the other code 
            Snap_Cast.TakeSnap_Static(Screen.width, Screen.height);
        }
    }
}
