using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapInput_Cast : MonoBehaviour
{
    public Button SnapButton;

    private void Start()
    {
        // When The button is clicked call the function SnapShot()
        SnapButton.onClick.AddListener(SnapShot);
    }
    void SnapShot()
    {
        {
            //Calling the function TakeSnap_Static() from the other script Snap_Cast 
            Snap_Cast.TakeSnap_Static(Screen.width, Screen.height);
        }
    }
}
//Inspo from Red Hen dev https://www.youtube.com/watch?v=Rx-fpP5IXho
