using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] GameObject[] platformPrefabs;
    GameObject centralPlatform, rightPlatform, topPlatform, leftPlatform, bottomPlatform, rightTopPlatform, rightBottomPlatform, leftTopPlatform, leftBottomPlatform;
    Transform player;
    int n = 0, m = 0;

    float playtformSize = 40.6f;

    GameObject getPlatform()
    {
        return platformPrefabs[Random.Range(0, platformPrefabs.Length)];
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;

        centralPlatform = Instantiate(getPlatform(), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));

        rightPlatform = Instantiate(getPlatform(), new Vector3(playtformSize, 0, 0), Quaternion.identity);
        topPlatform = Instantiate(getPlatform(), new Vector3(0, playtformSize, 0), Quaternion.identity);
        leftPlatform = Instantiate(getPlatform(), new Vector3(-playtformSize, 0, 0), Quaternion.identity);
        bottomPlatform = Instantiate(getPlatform(), new Vector3(0, -playtformSize, 0), Quaternion.identity);
        rightTopPlatform = Instantiate(getPlatform(), new Vector3(playtformSize, playtformSize, 0), Quaternion.identity);
        rightBottomPlatform = Instantiate(getPlatform(), new Vector3(playtformSize, -playtformSize), Quaternion.identity);
        leftTopPlatform = Instantiate(getPlatform(), new Vector3(-playtformSize, playtformSize), Quaternion.identity);
        leftBottomPlatform = Instantiate(getPlatform(), new Vector3(-playtformSize, -playtformSize), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.position.x - (n * playtformSize)) > playtformSize / 2)
        {
            n++;
            Destroy(leftPlatform);
            Destroy(leftTopPlatform);
            Destroy(leftBottomPlatform);

            leftPlatform = centralPlatform;
            leftTopPlatform = topPlatform;
            leftBottomPlatform = bottomPlatform;

            topPlatform = rightTopPlatform;
            centralPlatform = rightPlatform;
            bottomPlatform = rightBottomPlatform;

            rightTopPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n + 1), playtformSize * (m + 1)), Quaternion.identity);
            rightPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n + 1), playtformSize * m), Quaternion.identity);
            rightBottomPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n + 1), playtformSize * (m - 1)), Quaternion.identity);
        }
        else if ((player.position.x - (n * playtformSize)) < -playtformSize / 2)
        {
            n--;
            Destroy(rightPlatform);
            Destroy(rightTopPlatform);
            Destroy(rightBottomPlatform);

            rightPlatform = centralPlatform;
            rightTopPlatform = topPlatform;
            rightBottomPlatform = bottomPlatform;

            topPlatform = leftTopPlatform;
            centralPlatform = leftPlatform;
            bottomPlatform = leftBottomPlatform;

            leftTopPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n - 1), playtformSize * (m + 1)), Quaternion.identity);
            leftPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n - 1), playtformSize * m), Quaternion.identity);
            leftBottomPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n - 1), playtformSize * (m - 1)), Quaternion.identity);
        }
        else if ((player.position.y - (m * playtformSize)) > playtformSize / 2)
        {
            m++;
            Destroy(bottomPlatform);
            Destroy(rightBottomPlatform);
            Destroy(leftBottomPlatform);

            bottomPlatform = centralPlatform;
            rightBottomPlatform = rightPlatform;
            leftBottomPlatform = leftPlatform;

            rightPlatform = rightTopPlatform;
            centralPlatform = topPlatform;
            leftPlatform = leftTopPlatform;

            leftTopPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n - 1), playtformSize * (m + 1)), Quaternion.identity);
            topPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * n, playtformSize * (m + 1)), Quaternion.identity);
            rightTopPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n + 1), playtformSize * (m + 1)), Quaternion.identity);
        }
        else if ((player.position.y - (m * playtformSize)) < -playtformSize / 2)
        {
            m--;
            Destroy(topPlatform);
            Destroy(leftTopPlatform);
            Destroy(rightTopPlatform);

            topPlatform = centralPlatform;
            rightTopPlatform = rightPlatform;
            leftTopPlatform = leftPlatform;

            rightPlatform = rightBottomPlatform;
            centralPlatform = bottomPlatform;
            leftPlatform = leftBottomPlatform;

            leftBottomPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n - 1), playtformSize * (m - 1)), Quaternion.identity);
            bottomPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * n, playtformSize * (m - 1)), Quaternion.identity);
            rightBottomPlatform = Instantiate(getPlatform(), new Vector3(playtformSize * (n + 1), playtformSize * (m - 1)), Quaternion.identity);
        }
    }
}