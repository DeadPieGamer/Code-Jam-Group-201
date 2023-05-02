//Shady
using UnityEngine;
using System.Collections.Generic;

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

        //These are private fields that keep track of the current line being drawn and a list of points used to define the line.

        void Start() //This is the Start() method, which is called once when the script starts. If the Camera field is not set, it is set to the main camera.
        {
            if (!Cam)
            {
                Cam = Camera.main;
            }//if end
        }//Start() eend


        // Update is called once per frame. If the left mouse button is pressed, the CreateNewLine() method is called to start a new line. If the left mouse button is held down, the AddPoint()
        // method is called to add a new point to the current line. If the "R" key is pressed, all child objects of the transform are destroyed, effectively clearing the screen of lines.
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateNewLine();
            }//if end

            if (Input.GetMouseButton(0))
            {
                AddPoint();
            }//if end

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (transform.childCount != 0)
                {
                    foreach (Transform R in transform)
                    {
                        Destroy(R.gameObject);
                    }//loop end
                }//if end
            }//if end
            //UpdateLinePoints();
        }//Update() end

        private void CreateNewLine() //This is the CreateNewLine() method, which instantiates a new LineRenderer trail and sets its parent to the current object. The points list is cleared so that a new line can be drawn.
        {
            currentTrail = Instantiate(trailPrefab);
            currentTrail.transform.SetParent(transform, true);
            points.Clear();
        }//CreateCurrentTrail() end
         
        private void UpdateLinePoints() //This is the UpdateLinePoints() method, which updates the LineRenderer trail by setting its position count and positions.
        {
            if (currentTrail != null && points.Count > 1)
            {
                currentTrail.positionCount = points.Count;
                currentTrail.SetPositions(points.ToArray());
            }//if end
        }//UpdateTrailPoints() end

        private void AddPoint()
        {
            var Ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
                if (hit.collider.CompareTag("Writeable"))
                {
                    // points.Add(new Vector3(hit.point.x, 0f, hit.point.z));
                    points.Add(hit.point);
                    UpdateLinePoints();
                    return;
                }//if end
                else
                    return;
            }//if end
        }//AddPoint() end

    }//class end
}//namespace end
