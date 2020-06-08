using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianSpawner : MonoBehaviour
{
    public GameObject civilian = null;
    public int maxSpawnCount = 50;
    public float SpawnRadius = 20f;

    public GameObject offset;
   
    private IEnumerator Start()
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            //pick random spawn position
            Vector3 spawnPosition = offset.transform.position + (Random.insideUnitSphere * SpawnRadius);
            spawnPosition.y = 0;
            //with random rotation
            Instantiate(civilian, spawnPosition, Quaternion.Euler(0, Random.value * 360, 0));
            yield return new WaitForSeconds(Random.value * 0.1f);
        }
    }
}
