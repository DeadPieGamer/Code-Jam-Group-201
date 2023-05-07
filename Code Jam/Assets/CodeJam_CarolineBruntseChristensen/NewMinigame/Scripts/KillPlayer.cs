using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KillPlayer : MonoBehaviour
{
    //Code inspired by "bblakeyyy" on YT, link: https://www.youtube.com/watch?v=H69PfxOr6bk&t=40s
    [SerializeField] private int Respawn;
    [SerializeField] private int distanceTraveled;
    private int pointDeduct = 200;
    private int divider = 100;
    private int winningScore = 1000;

    [SerializeField] private TextMeshPro text;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))//if anything that has the tag Obstacle then this will happen
        {
            distanceTraveled -= pointDeduct;
        }
    }

    private void FixedUpdate()
    {
        distanceTraveled++;
        Finish(distanceTraveled);
        text.text = (distanceTraveled / divider).ToString()+" km";
    }
    void Finish(int points)
    {
        if(points == winningScore)
        {
            SceneManager.LoadScene(Respawn);
        }
    }
}
