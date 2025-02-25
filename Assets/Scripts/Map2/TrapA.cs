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

    public Vector2 restrictedMin = new Vector2(-1f, -5f); // 생성 금지 영역 최소 좌표
    public Vector2 restrictedMax = new Vector2(1f, -3.4f);  // 생성 금지 영역 최대 좌표

    public float obstacleRadius = 0.5f; // 장애물의 반지름 (겹치지 않게 하기 위해 사용)

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // 생성된 장애물 리스트

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        int obstacleCount = Random.Range(minObstacles, maxObstacles + 1);
        spawnedObstacles.Clear();

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 spawnPosition;
            int maxAttempts = 20; // 무한 루프 방지를 위한 최대 시도 횟수
            int attempts = 0;

            do
            {
                spawnPosition = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );
                attempts++;
            }
            while ((IsInsideRestrictedArea(spawnPosition) || IsOverlapping(spawnPosition)) && attempts < maxAttempts);

            if (attempts < maxAttempts)
            {
                GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                spawnedObstacles.Add(newObstacle);
            }
        }
    }

    bool IsInsideRestrictedArea(Vector2 position)
    {
        return position.x >= restrictedMin.x && position.x <= restrictedMax.x &&
               position.y >= restrictedMin.y && position.y <= restrictedMax.y;
    }

    bool IsOverlapping(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, obstacleRadius) != null;
    }

    public void ResetObstacles()
    {
        // 기존 장애물 제거
        foreach (var obstacle in spawnedObstacles)
        {
            if (obstacle != null)
                Destroy(obstacle);
        }

        // 새 장애물 생성
        SpawnObstacles();
    }
}