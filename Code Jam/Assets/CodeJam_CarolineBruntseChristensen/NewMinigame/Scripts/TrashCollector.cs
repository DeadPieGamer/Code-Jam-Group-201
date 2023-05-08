using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("TrashCollector")) Destroy(this.gameObject);

    }
}

