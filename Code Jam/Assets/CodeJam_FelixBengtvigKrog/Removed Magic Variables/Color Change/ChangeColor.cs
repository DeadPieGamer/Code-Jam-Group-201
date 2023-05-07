using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    // Code inspired by "GameAssetWorld", YT link: https://www.youtube.com/watch?v=EzZGPRBchJo

    public Image image;
    public Sprite[] sprites;

    public Animator okAnimator;

    public Button okButton;

    // Made a static string for the name of the PlayerPref int that stores the color choice.
    [HideInInspector] public static string colorPlayerPref { get; private set; } = "Picked Color";

    public void colorChange(int button)
    {
        // Removed an unnecessary variable
        image.sprite = sprites[button];
        // Replaced the hardcoded PlayerPref name with a variable
        PlayerPrefs.SetInt(colorPlayerPref, button);
        okAnimator.SetBool("Moving", true);
        // Changed from enabling entire button component to just making it interactable
        okButton.interactable = true;
    }
    private void Start()
    {
        // Changed from disabling entire button component to just making it not interactable. I just think it looks nicer as it becomes semi-transparent with this
        okButton.interactable = false;
    }
}
