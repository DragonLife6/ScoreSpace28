using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Tooltip("Stops all enemies from spawning")]
    [SerializeField] bool testMode = false;

    [SerializeField] GameObject[] enemyPrefabs;

    int maxEnemies = 1; // Count of enemies spawned every "spawnInterval" seconds
    float spawnInterval = 1.0f;
    float maxHPCoef = 1f;
    float sizeCoef = 1f;
    float movementSpeedCoef = 1f;
    float damageCoef = 1f;
    float superMutationChance = 0.01f;

    [Tooltip("Spawn distance from player position")]
    public float spawnDistance = 20.0f;

    [Tooltip("Max distance from player. Don't try to change when game is running")]
    public float maxDistance = 30.0f;

    int minEnemyLvl = 0;
    int maxEnemyLvl = 1;

    [SerializeField] Transform player; // Player position
    
    private List<Transform> enemies = new List<Transform>(); // List of all enemies

    float nextParameterChangeTime; // For changing difficulty
    [SerializeField] float changeInterval = 30f;

    int mutationsCount;

    void Start()
    {
        mutationsCount = 0;
        if (!testMode)
        {
            InvokeRepeating("SpawnEnemies", 0.1f, spawnInterval);
        }

        nextParameterChangeTime = Time.time + changeInterval;
    }

    private void ChangeParameters()
    {
        mutationsCount++;
        int randomDistNum = Random.Range(0, 100);

        if (randomDistNum < 5) // 5% chance
        {
            maxEnemies++;

            Debug.Log("+ max enemies");
        }
        else if (randomDistNum < 10) // 5% chance
        {
            spawnInterval *= 0.95f;
            Debug.Log("- spawn interval");
        }
        else if (randomDistNum < 50) // 40% chance
        {
            maxHPCoef *= 1.1f;
            sizeCoef *= 1.1f;
            Debug.Log("+ max health" + maxHPCoef.ToString());
        }
        else if (randomDistNum < 80) // 30% chance
        {
            movementSpeedCoef *= 1.05f;
            Debug.Log("+ move speed");
        }
        else // 20% chance
        {
            damageCoef *= 1.05f;
            Debug.Log("+ damage");
        }

        superMutationChance += 0.01f;

        if(mutationsCount >= 5)
        {
            maxEnemyLvl++;
            mutationsCount = 0;
            if (maxEnemyLvl < enemyPrefabs.Length)
            {
                sizeCoef = 1f;
                maxHPCoef = 1f + 0.1f * (maxEnemyLvl - 1);
                movementSpeedCoef = 1f + 0.1f * (maxEnemyLvl - 2);
                damageCoef = 1f;
                superMutationChance = 0.03f;
            }
        }
        if(maxEnemyLvl >= 3)
        {
            minEnemyLvl = maxEnemyLvl - 2;
        }

    }

    private void Update()
    {
        if (Time.time >= nextParameterChangeTime)
        {
            ChangeParameters();
            nextParameterChangeTime = Time.time + changeInterval;
            if (changeInterval >= 7f)
            {
                changeInterval *= 0.9f;
            }
        }
    }

    public void AddEnemy(Transform newEnemy)
    {
        enemies.Add(newEnemy);
    }

    public void SpawnEnemy(GameObject enemyRef)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newEnemy = Instantiate(enemyRef, spawnPosition, Quaternion.identity);
        newEnemy.SendMessage("ApplyMaxHealthCoef", maxHPCoef);
        newEnemy.SendMessage("ApplySizeCoef", sizeCoef);
        newEnemy.SendMessage("ApplyDamageCoef", damageCoef);
        newEnemy.SendMessage("ApplySpeedCoef", movementSpeedCoef);
        enemies.Add(newEnemy.transform);
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
            GameObject newEnemy = Instantiate(enemyPrefabs[randomNum], spawnPosition, Quaternion.identity);
            newEnemy.SendMessage("ApplyMaxHealthCoef", maxHPCoef);
            newEnemy.SendMessage("ApplyDamageCoef", damageCoef);
            newEnemy.SendMessage("ApplySpeedCoef", movementSpeedCoef);
            newEnemy.SendMessage("ApplyMutationChance", superMutationChance); // Visual mutation
            enemies.Add(newEnemy.transform);
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
