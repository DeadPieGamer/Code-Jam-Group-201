using UnityEngine;
using UnityEngine.InputSystem;

public class Follow_Mouse : MonoBehaviour
{
    // Changed to use new input system
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void MoveToInput(InputAction.CallbackContext context)
    {
        transform.position = (Vector2)mainCam.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    
}