using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapA : MonoBehaviour
{
    public GameObject obstaclePrefab; // ��ֹ� ������
    public int minObstacles = 2;  // �ּ� ��ֹ� ����
    public int maxObstacles = 6; // �ִ� ��ֹ� ����
    public Vector2 spawnAreaMin = new Vector2(-2.4f, -4.4f);  // ���� ���� �ּ� ��ǥ
    public Vector2 spawnAreaMax = new Vector2(2.4f, 3.5f);  // ���� ���� �ִ� ��ǥ

    private GameObject[] spawnedObstacles;  // ������ ��ֹ����� ���� �迭

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
        // ���� ��ֹ� ����
        if (spawnedObstacles != null)
        {
            foreach (var obstacle in spawnedObstacles)
            {
                if (obstacle != null)
                    Destroy(obstacle);
            }
        }

        // �� ��ֹ� ����
        SpawnObstacles();
    }

}