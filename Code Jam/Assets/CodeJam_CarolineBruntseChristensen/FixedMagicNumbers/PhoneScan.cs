using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneScan : MonoBehaviour
{
    [SerializeField] private Animator scannerAnimator;
    [SerializeField] private LoadScene sceneMang;
    [SerializeField] private float maxAcceleration = 0.3f;
    private int currentShakeCount = 0;
    private int maxShakes = 3;

    private string scanAnimation = "Scanning";

    private float gravity = 0.98f;

    private float shortwait = 1f;
    private float longwait = 5f;

    public void CheckAccelerometer(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        float acceleration = context.ReadValue<Vector3>().magnitude;

        acceleration -= gravity;

        // If acceleration is too high or too low, stop the scan
        if (acceleration > maxAcceleration || acceleration < -maxAcceleration)
        {
            Debug.Log(acceleration);
            // Remember this shake
            currentShakeCount++;
            // If it has been shaked the max amount of times, stop the scan
            if (currentShakeCount > maxShakes)
            {
                Debug.Log("Reached this");
                // Stop the scan
                StopAllCoroutines();
                // Tell the animator I ain't scanning anymore
                scannerAnimator.SetBool(scanAnimation, false);
            }
        }
    }
    
    public void buttonScan()
    {
        // Reset counted shakes
        currentShakeCount = 0;
        StartCoroutine(StartScan());
    }

    private IEnumerator StartScan()
    {
        scannerAnimator.SetBool(scanAnimation, true);
        yield return new WaitForSeconds(shortwait);
        yield return new WaitForSeconds(longwait);
        sceneMang.LoadNextScene();
    }
}
