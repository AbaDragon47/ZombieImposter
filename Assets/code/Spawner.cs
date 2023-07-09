using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay;
    public bool move;
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
            if(move)
            {
                spawnDelay = Random.Range(8f, 15f);
                Transform spawnLocation = GetComponent<Transform>();

                Instantiate(prefabToSpawn, spawnLocation.position, prefabToSpawn.GetComponent<Transform>().rotation);
            }
            else
            {
                spawnDelay = Random.Range(6f,8f);

                Instantiate(prefabToSpawn,new Vector3(gameObject.transform.position.x+Random.Range(-3f,4f),gameObject.transform.position.y+Random.Range(-3f,4f),0),prefabToSpawn.GetComponent<Transform>().rotation);
            }
            yield return new WaitForSeconds(spawnDelay);
            
        }
    }
    
}
