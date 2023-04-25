using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_CastGame : MonoBehaviour
{
    [SerializeField] private Slicable_Cast slicable;
    [SerializeField] private GameObject sliced;

    public void OnMouseExit() {
        if(slicable.getSlicedObject() == null){
            slicable.setSlicedObject(sliced);
            slicable.Slice();
        }
    }
}
