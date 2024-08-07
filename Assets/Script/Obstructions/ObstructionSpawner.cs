using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionSpawner : MonoBehaviour
{
    public GameObject barrel;
    public float barrelSpawnInterval;
    public GameObject Jumper;
    public float jumperSpawnInterval;
    public GameObject follow;
    public int maxFollow;
    public float followSpawnInterval;

    public GameObject[] spawnPoint;

    private void Start()
    {
        StartCoroutine(BarrelSpawn());
        StartCoroutine(JumperSpawn());
    }


    IEnumerator BarrelSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomSpawnTime(barrelSpawnInterval));
            Instantiate(barrel, RandomSpawnPoint(), Quaternion.identity);
        }
    }
    IEnumerator JumperSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomSpawnTime(jumperSpawnInterval));
            Instantiate(Jumper, RandomSpawnPoint(), Quaternion.identity);
        }
    }

    float RandomSpawnTime(float interval)
    {
        return Random.Range(0.5f, interval);
    }
    Vector3 RandomSpawnPoint()
    {
        int index = Random.Range(0, 2);
        return spawnPoint[index].transform.position;
    }


}
