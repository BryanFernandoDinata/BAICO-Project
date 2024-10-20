using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public Transform[] spawnPos;
    public GameObject[] objectsToSpawn;

    private void Start() 
    {
        Spawn();
    }
    public void Spawn()
    {
        for(int i = 0; i < spawnPos.Length; i++)
        {
//            Debug.Log(i);
            Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawnPos[i]);
        }
    }
}
