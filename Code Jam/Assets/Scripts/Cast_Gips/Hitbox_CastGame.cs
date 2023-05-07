using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_CastGame : MonoBehaviour
{
    //reference of our Slicable_Cast script 
    [SerializeField] private Slicable_Cast slicable;

    //gameobject that contains the gameobject with the sliced cast image
    [SerializeField] private GameObject sliced;


    public void OnMouseExit()
    {
        // if object has not been sliced then...
        if(slicable.getSlicedObject() == null)
        {
            slicable.setSlicedObject(sliced);
            //calling the function from our other script
            slicable.Slice();
            //Debug.Log("came from" + transform.name);
        }
    }
}
//Inspo from 1 Minute Unity youtube.com/watch?v=muPZvw7CU-0&t=509s