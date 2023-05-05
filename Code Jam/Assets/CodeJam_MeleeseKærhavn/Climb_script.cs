using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Climb_script : MonoBehaviour
{
    public GameObject næsteKlatre;
    public Button test;


    public void NextScene()
    {
        næsteKlatre.SetActive(true);
        // Debug.Log("Is Clicked");
    }

}

