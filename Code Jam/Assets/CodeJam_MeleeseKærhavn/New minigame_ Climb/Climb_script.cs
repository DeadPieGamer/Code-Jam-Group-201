using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Climb_script : MonoBehaviour
{
    public GameObject nęsteKlatre;
    public Button test;


    public void NextScene()
    {
        nęsteKlatre.SetActive(true);
        Debug.Log("Is Clicked");
    }

}

