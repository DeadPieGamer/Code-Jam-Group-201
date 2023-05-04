using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapInput_Cast : MonoBehaviour
{
    [SerializeField] Button SnapButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Calling the funktion from the other code 
            Snap_Cast.TakeSnap_Static(Screen.width, Screen.height);
        }
    }
}
