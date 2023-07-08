using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        spawnDelay=5f;
        StartCoroutine(SpawnPrefabs());
    }
    //can acts between frames without stopping other oncurring code
    IEnumerator SpawnPrefabs()
    {
        while (true)
        {
            spawnDelay = Random.Range(0, 5f);
            Transform spawnLocation = GetComponent<Transform>();

            Instantiate(prefabToSpawn, spawnLocation.position, prefabToSpawn.GetComponent<Transform>().rotation);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    
}
