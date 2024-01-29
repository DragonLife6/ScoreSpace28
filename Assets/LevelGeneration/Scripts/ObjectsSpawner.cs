using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int maxObjects = 10;

    public float despawnDistance = 20f;
    public float outerRadius = 10f;
    public float innerRadius = 5f;

    private GameObject GetObjectPrefab()
    {
        int objNum;
        int objProbability = Random.Range(0, 100);

        if(objProbability < 50)
        {
            objNum = 0;
        } else if (objProbability < 80)
        {
            objNum = 1;
        } else
        {
            objNum = 2;
        }

        if(objNum >= objectPrefabs.Length)
        {
            objNum = 0;
        }

        return objectPrefabs[objNum];
    }

    private void Start()
    {
        SpawnObjects();
    }

    Vector2 GetRandomPositionOnRing(float inner, float outer)
    {
        float angle = Random.Range(0f, 360f);
        float radius = Random.Range(inner, outer);

        float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);

        return new Vector2(x, y);
    }


    void SpawnObjects()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            Vector2 randomRingPosition = GetRandomPositionOnRing(innerRadius, outerRadius);
            Vector3 spawnPos = transform.position + new Vector3(randomRingPosition.x, randomRingPosition.y, 0f);
          
            Instantiate(GetObjectPrefab(), spawnPos, Quaternion.identity);
        }

        Invoke(nameof(DespawnObjects), 0.1f);
    }

    void DespawnObjects()
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");

        foreach (var obj in spawnedObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance > despawnDistance)
            {
                Vector2 randomRingPosition = GetRandomPositionOnRing(innerRadius, outerRadius);
                Vector3 spawnPos = transform.position + new Vector3(randomRingPosition.x, randomRingPosition.y, 0f);
                Instantiate(GetObjectPrefab(), spawnPos, Quaternion.identity);

                Destroy(obj);
            }
        }

        if (spawnedObjects.Length < maxObjects)
        {
            for (int i = spawnedObjects.Length; i < maxObjects; i++)
            {
                Vector2 randomRingPosition = GetRandomPositionOnRing(innerRadius, outerRadius);
                Vector3 spawnPos = transform.position + new Vector3(randomRingPosition.x, randomRingPosition.y, 0f);
                Instantiate(GetObjectPrefab(), spawnPos, Quaternion.identity);
            }
        }

        Invoke(nameof(DespawnObjects), 0.1f);
    }
}
