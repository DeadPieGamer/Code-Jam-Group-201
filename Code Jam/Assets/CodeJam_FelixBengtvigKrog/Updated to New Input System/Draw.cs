//Shady
using UnityEngine;
using System.Collections.Generic;
// Change: Updating this to use the new input system
using UnityEngine.InputSystem;

namespace Shady
{
    // Code Inspired by "Education Promotions", YT link: https://www.youtube.com/watch?v=-Otaxxn5N2M

    public class Draw : MonoBehaviour
    {
        [SerializeField] Camera Cam = null;
        [SerializeField] LineRenderer trailPrefab = null;

        //These are serialized private fields. They can be edited in the Inspector and accessed by other scripts. The Camera field represents the camera that the raycast will originate from,
        //and the LineRenderer field is the trail prefab used to create the line.

        private LineRenderer currentTrail;
        private List<Vector3> points = new List<Vector3>();

        private bool currentlyDrawing = false;

        //These are private fields that keep track of the current line being drawn and a list of points used to define the line.

        void Start() //This is the Start() method, which is called once when the script starts. If the Camera field is not set, it is set to the main camera.
        {
            if (!Cam)
            {
                Cam = Camera.main;
            }
        }

        // Removed all Update() input handling to favour the new input system

        public void CreateNewLine(InputAction.CallbackContext context) //This is the CreateNewLine() method, which instantiates a new LineRenderer trail and sets its parent to the current object. The points list is cleared so that a new line can be drawn.
        {
            // If the input begins, set currentlyDrawing to true
            if (context.started)
            {
                currentlyDrawing = true;
            }
            else if (context.canceled) // If the input ends, stop drawing
            {
                currentlyDrawing = false;
                return;
            }
            else // If the input is registered in any other way, end the function (We only want this function to be called once when an input begins)
            {
                return;
            }

            currentTrail = Instantiate(trailPrefab);
            currentTrail.transform.SetParent(transform, true);
            points.Clear();
        }

        public void UpdateLinePoints() //This is the UpdateLinePoints() method, which updates the LineRenderer trail by setting its position count and positions.
        {
            if (currentTrail != null && points.Count > 1)
            {
                currentTrail.positionCount = points.Count;
                currentTrail.SetPositions(points.ToArray());
            }
        }

        public void DrawLine(InputAction.CallbackContext context)
        {
            // If not currently drawing, return
            if (!currentlyDrawing) return;

            Ray myRay = Cam.ScreenPointToRay(context.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(myRay, out hit))
            {
                if (hit.collider.CompareTag("Writeable"))
                {
                    points.Add(hit.point);
                    UpdateLinePoints();
                }
            }
        }

        // Changed this to a function so a reset button can be added. Was previously hardcoded to happen on R-button presses, which made no sense as this is intended for tablets
        public void ResetDrawing()
        {
            if (transform.childCount != 0)
            {
                foreach (Transform R in transform)
                {
                    Destroy(R.gameObject);
                }
            }
        }

    }
}
