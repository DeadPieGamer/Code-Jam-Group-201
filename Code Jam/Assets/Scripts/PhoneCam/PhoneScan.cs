using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneScan : MonoBehaviour
{
    [SerializeField] private Animator scannerAnimator;
    [SerializeField] private LoadScene sceneMang;
    [SerializeField] private float maxAcceleration = 0.3f;//Max/Min range that the acceleration can be in
    private int currentShakeCount = 0;
    private int maxShakes = 3;

    private float gravity = 0.98f; //earth garvity is around 9.82m/s^2 hench the 0.98f

    private float shortwait = 1f; 
    private float longwait = 5f;

    private string conditionName = "Scanning";

    /// <summary>
    /// If the acceleration is more than max/min amount it stop the scan
    /// </summary>
    /// <param name="context"></param>
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
                //Debug.Log("Reached this");
                // Stop the scan
                StopAllCoroutines();
                // Tell the animator I ain't scanning anymore
                scannerAnimator.SetBool(conditionName, false);
            }
        }
    }
    
    /// <summary>
    /// When this function gets called the currentShakeCount gets reset and start the StartScan Ienumerator
    /// </summary>
    public void buttonScan()
    {
        // Reset counted shakes
        currentShakeCount = 0;
        StartCoroutine(StartScan());
    }

    /// <summary>
    /// This Ienumerator start the scanning animation and load next scene after the wait time is over
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartScan()
    {
        scannerAnimator.SetBool(conditionName, true);
        yield return new WaitForSeconds(shortwait);
        yield return new WaitForSeconds(longwait);
        sceneMang.LoadNextScene();
    }
}
