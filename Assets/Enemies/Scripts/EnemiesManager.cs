using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Tooltip("Stops all enemies from spawning")]
    [SerializeField] bool testMode = false;

    [SerializeField] GameObject[] enemyPrefabs;
    int maxEnemies = 1; // Count of enemies spawned every "spawnInterval" seconds
    float spawnInterval = 1.0f;

    [Tooltip("Spawn distance from player position")]
    public float spawnDistance = 20.0f;

    [Tooltip("Max distance from player. Don't try to change when game is running")]
    public float maxDistance = 30.0f;

    int minEnemyLvl = 0;
    int maxEnemyLvl = 1;

    [SerializeField] Transform player; // Player position
    
    private List<Transform> enemies = new List<Transform>(); // List of all enemies

    float nextParameterChangeTime; // For changing difficulty

    void Start()
    {
        if (!testMode)
        {
            InvokeRepeating("SpawnEnemies", 0.1f, spawnInterval);
        }

        nextParameterChangeTime = Time.time + 30f;
    }

    private void ChangeParameters()
    {
        maxEnemies++;
        spawnInterval *= 0.95f;
    }

    private void Update()
    {
        if (Time.time >= nextParameterChangeTime)
        {
            ChangeParameters();
            nextParameterChangeTime = Time.time + 30f;
        }


        // Respawn enemies if they are too far from player
        /*
        for (int i = 0; i < enemies.Count; i++)
        {
            if (GetDistance(enemies[i]) > maxDistance)
            {
                GameObject enemyRef = enemies[i].gameObject;
                if (enemyRef != null)
                {
                    Destroy(enemyRef);
                    SpawnEnemy(enemyRef);
                }
            }
        }*/
    }

    public void AddEnemy(Transform newEnemy)
    {
        enemies.Add(newEnemy);
    }

    public void SpawnEnemy(GameObject enemyRef)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Transform newEnemy = Instantiate(enemyRef, spawnPosition, Quaternion.identity).transform;
        enemies.Add(newEnemy);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if (maxEnemyLvl > enemyPrefabs.Length || maxEnemyLvl < minEnemyLvl)
            {
                maxEnemyLvl = enemyPrefabs.Length;
            }

            int randomNum = Random.Range(minEnemyLvl, maxEnemyLvl);

            Vector3 spawnPosition = GetRandomSpawnPosition();
            Transform newEnemy = Instantiate(enemyPrefabs[randomNum], spawnPosition, Quaternion.identity).transform;

            enemies.Add(newEnemy);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        int angle = Random.Range(0, 360);
        Vector3 playerPosition;
        if (player != null)
        {
            playerPosition = player.position;
        } else
        {
            playerPosition = Vector3.zero;
        }

        Vector2 randomOffset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * spawnDistance;
        Vector3 spawnPosition = playerPosition + new Vector3(randomOffset.x, randomOffset.y, 0);

        return spawnPosition;
    }

    public void DeleteEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }

    /* Sortin and returning of closest to player enemies
    public List<Transform> GetClossestEnemies()
    {
        SortList();
        return enemies;
    }

    
    void SortList()
    {
        int minId;
        int startLength = enemies.Count;
        List<Transform> newList = new List<Transform>();

        for (int i = 0; i < startLength; i++)
        {
            minId = 0;
            for (int j = 1; j < enemies.Count; j++)
            {
                if (GetDistance(enemies[j]) < GetDistance(enemies[minId]))
                {
                    minId = j;
                }
            }

            newList.Add(enemies[minId]);
            enemies.Remove(enemies[minId]);
        }

        enemies = newList;
    }
    */

    float GetDistance(Transform transform)
    {
        if (transform != null)
        {
            return Vector3.Distance(player.position, transform.position);
        }
        else
        {
            return 0f;
        }
    }
}
