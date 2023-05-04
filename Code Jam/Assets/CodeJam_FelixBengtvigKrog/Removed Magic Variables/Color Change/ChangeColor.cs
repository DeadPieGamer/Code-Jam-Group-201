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

    // Made a static string for the name of the PlayerPref int that stores the color choice.
    [HideInInspector] public static string colorPlayerPref { get; private set; } = "Picked Color";

    public void colorChange(int button)
    {
        sprite = sprites[button];
        image.sprite = sprite;
        // Replaced the hardcoded PlayerPref name with a variable
        PlayerPrefs.SetInt(colorPlayerPref, button);
        okAnimator.SetBool("Moving", true);
        okButton.enabled = true;
    }
    private void Start()
    {
        okButton.enabled = false;
    }
}
