using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeByNum : MonoBehaviour
{
    // Code inspired by "GameAssetWorld", YT link: https://www.youtube.com/watch?v=EzZGPRBchJo

    public Color color;
    public Color[] colors;
    public SpriteRenderer CastColor;


    public void colorChange(int button)
    {
        color = colors[button];
        CastColor.color = color;
    }
    private void Start()
    {
        colorChange(PlayerPrefs.GetInt("Picked Color"));
    }
}
