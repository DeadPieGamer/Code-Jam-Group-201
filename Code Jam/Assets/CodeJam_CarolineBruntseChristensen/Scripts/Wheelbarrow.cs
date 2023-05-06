using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelbarrow : MonoBehaviour
{
    //Code inspired by "Alexander Zotov" on YT link: https://www.youtube.com/watch?v=wpSm2O2LIRM
    
    Rigidbody2D rb;
    float dirX;
    float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //Assign our Rigidbody to the component
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }
}
