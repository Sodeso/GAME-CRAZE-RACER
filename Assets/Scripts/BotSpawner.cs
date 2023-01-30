using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    GameObject bot;
    GameObject cloneBot;
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
        bot = GameObject.Find("GameManager").GetComponent<GameManager>().listBots[UnityEngine.Random.Range(0, GameObject.Find("GameManager").GetComponent<GameManager>().listBots.Count)];
        cloneBot = Instantiate(bot, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
        cloneBot.SetActive(true);
    }

}
