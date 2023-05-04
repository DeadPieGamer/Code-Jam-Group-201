using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetImageColor : MonoBehaviour
{
    public int colorNumber;

    // Start is called before the first frame update
    void Start()
    {
        // Replaced the hardcoded PlayerPref name with a variable
        colorNumber = PlayerPrefs.GetInt(ChangeColor.colorPlayerPref);
        Debug.Log(colorNumber);
    }
}
