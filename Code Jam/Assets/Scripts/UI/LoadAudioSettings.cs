using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAudioSettings : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
