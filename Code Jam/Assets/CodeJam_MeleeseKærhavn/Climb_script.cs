using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Climb_script : MonoBehaviour
{
    public GameObject n�steKlatre;
    public Button test;


    public void NextScene()
    {
        n�steKlatre.SetActive(true);
        // Debug.Log("Is Clicked");
    }

}

