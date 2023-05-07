using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastColor : MonoBehaviour
{
  
    // Code inspired by "GameAssetWorld", YT link: https://www.youtube.com/watch?v=EzZGPRBchJo

    public Sprite[] sprites;
    public SpriteRenderer castRender;
   

    public void colorChange(int button)
    {
        // Removed an unnecessary variable
        castRender.sprite = sprites[button];
    }
    private void Start()
    {
        // Using the static variable to get the playerpref and added a default value
        colorChange(PlayerPrefs.GetInt(ChangeColor.colorPlayerPref, 0));
    }
}

