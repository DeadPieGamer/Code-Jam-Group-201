using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    // Code inspired by "GameAssetWorld", YT link: https://www.youtube.com/watch?v=EzZGPRBchJo

    public Image imageColor;
    public Color color;
    public Color[] colors;
    

    public void colorChange(int button)
    {
        color = colors[button];
        imageColor.color = color; 
    }


   
}
