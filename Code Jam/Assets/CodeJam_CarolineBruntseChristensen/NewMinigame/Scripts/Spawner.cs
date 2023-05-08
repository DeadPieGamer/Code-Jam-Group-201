using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Code inspired by "Magic Coding" on Yt, link:https://www.youtube.com/watch?v=_Ci1ouj8ip8

    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private float xBounds, yBounds;

    [SerializeField] private int secToSpawn = 1;

    [SerializeField] private float spawnChance = .9f;

    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(secToSpawn);

        int randomObstacles = Random.Range(0, obstacles.Length);

        if (Random.value <= spawnChance)
        {
            Instantiate(obstacles[randomObstacles],
            new Vector2(Random.Range(-xBounds, xBounds), yBounds), Quaternion.identity);
        }
            

        StartCoroutine(SpawnRandomGameObject());
    }
 
}
