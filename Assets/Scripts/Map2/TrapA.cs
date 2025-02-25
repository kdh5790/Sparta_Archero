using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapA : MonoBehaviour
{
    public GameObject obstaclePrefab; // 장애물 프리팹
    public int minObstacles = 2;  // 최소 장애물 개수
    public int maxObstacles = 6; // 최대 장애물 개수
    public Vector2 spawnAreaMin = new Vector2(-2.4f, -4.4f);  // 스폰 영역 최소 좌표
    public Vector2 spawnAreaMax = new Vector2(2.4f, 3.5f);  // 스폰 영역 최대 좌표

    private GameObject[] spawnedObstacles;  // 생성된 장애물들을 담을 배열

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        int obstacleCount = Random.Range(minObstacles, maxObstacles + 1);
        spawnedObstacles = new GameObject[obstacleCount];

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            spawnedObstacles[i] = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void ResetObstacles()
    {
        // 기존 장애물 제거
        if (spawnedObstacles != null)
        {
            foreach (var obstacle in spawnedObstacles)
            {
                if (obstacle != null)
                    Destroy(obstacle);
            }
        }

        // 새 장애물 생성
        SpawnObstacles();
    }

}