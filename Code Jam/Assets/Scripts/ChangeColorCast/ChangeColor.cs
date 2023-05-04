using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    // Code inspired by "GameAssetWorld", YT link: https://www.youtube.com/watch?v=EzZGPRBchJo

    public Image image;
    public Sprite sprite;
    public Sprite[] sprites;

    public Animator okAnimator;

    public Button okButton;

    string prefColor = CastColor.prefColorName;
    string conditionName = "Moving";

    public void colorChange(int button)
    {
        sprite = sprites[button];
        image.sprite = sprite;
        PlayerPrefs.SetInt(prefColor, button);
        okAnimator.SetBool(conditionName, true);
        okButton.enabled = true;
    }
    private void Start()
    {
        okButton.enabled = false;
    }
}
