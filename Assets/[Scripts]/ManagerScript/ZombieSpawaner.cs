using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawaner : MonoBehaviour
{
    public int numberofZombiesToSpawn;

    public GameObject[] zombiePrefabs;

    public SpawningVolume[] spawningVolumes;

    private GameObject followGameObject;
    // Start is called before the first frame update
    void Start()
    {
        followGameObject = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < numberofZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }
    
    void SpawnZombie()
    {
        GameObject zombieToSpwan = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        SpawningVolume spawnVolume = spawningVolumes[Random.Range(0, spawningVolumes.Length)];
        //if (!followGameObject) return;
        
        //object pooling can be referenced here
        GameObject zombie =
            Instantiate(zombieToSpwan, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);
        //zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
    }
}
