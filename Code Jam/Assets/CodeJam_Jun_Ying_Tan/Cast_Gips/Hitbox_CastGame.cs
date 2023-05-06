using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_CastGame : MonoBehaviour
{
    //reference of our Slicable_Cast script 
    [SerializeField] private Slicable_Cast slicable;

    //gameobject

    [SerializeField] private GameObject sliced;

    public void OnMouseExit() {
        if(slicable.getSlicedObject() == null){
            slicable.setSlicedObject(sliced);
            slicable.Slice();
            //Debug.Log("came from" + transform.name);
        }
    }
}
//Inspo from 1 Minute Unity youtube.com/watch?v=muPZvw7CU-0&t=509s