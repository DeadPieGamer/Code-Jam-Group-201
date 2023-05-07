using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetImageColor : MonoBehaviour
{
    public int colorNumber;

    // Start is called before the first frame update
    void Start()
    {
        colorNumber = PlayerPrefs.GetInt("Picked Color");
        Debug.Log(colorNumber);
    }
}
