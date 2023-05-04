using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

public class CastColor : MonoBehaviour
{

    // Code inspired by "GameAssetWorld", YT link: https://www.youtube.com/watch?v=EzZGPRBchJo
    public static string prefColorName = "Picked Color";
    public Sprite sprite;
    public Sprite[] sprites;
    public SpriteRenderer castRender;
   

    public void colorChange(int button)
    {
        sprite = sprites[button];
        castRender.sprite = sprite;
    }
    private void Start()
    {
        colorChange(PlayerPrefs.GetInt(prefColorName));
    }
}

