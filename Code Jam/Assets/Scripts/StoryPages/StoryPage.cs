using System;
using UnityEngine;
[Serializable]
public struct StoryPage
{
    [SerializeField, Tooltip("The image for this panel")] public Sprite pageGraphic;
    [SerializeField, Tooltip("An audioclip that should be played along with the panel")] public AudioClip clip;
    [SerializeField, Tooltip("The time this image should be shown"), Min(0f)] public float specialWaitTime;
}
