using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    GameObject gold;
    GameObject cloneGold;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;

    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    private void Spawn()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        gold = GameObject.Find("GameManager").GetComponent<GameManager>().goldObject;
        cloneGold = Instantiate(gold, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
        cloneGold.SetActive(true);
    }
}
