using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Follow_Mouse : MonoBehaviour
{
    public void Update()
    {
        //container for the the cursor's position 
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //setting the objcet position to the cursor's position 
        transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }
    
}
//inspo from Michael https://www.youtube.com/watch?v=0Qy3l3VuF_o