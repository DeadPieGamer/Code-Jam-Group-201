using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroll : MonoBehaviour
{
    // Code inspired by "Adamant Algorithm" on YT link: https://www.youtube.com/watch?v=U72trwZ7AT8&list=LL&index=1&t=185s

    [SerializeField] private float speed = 4f;
    private Vector3 StartPosition;
    private float bgOutofFramePos = -14.80f;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position; //When you start the game the BG would be
    }

    // Update is called once per frame
    void Update() //it happens again and again 
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); //This will move the background downwards
        if(transform.position.y < bgOutofFramePos)//the moment the original BG image goes any value below -14.80 the position of it is reset to the same place that it was at the start of the game
        { 
            transform.position = StartPosition;
        }
    }
}
