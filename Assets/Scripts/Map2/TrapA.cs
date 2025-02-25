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

    public Vector2 restrictedMin = new Vector2(-1f, -5f); // ���� ���� ���� �ּ� ��ǥ
    public Vector2 restrictedMax = new Vector2(1f, -3.4f);  // ���� ���� ���� �ִ� ��ǥ

    public float obstacleRadius = 0.5f; // ��ֹ��� ������ (��ġ�� �ʰ� �ϱ� ���� ���)

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // ������ ��ֹ� ����Ʈ

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
            int maxAttempts = 20; // ���� ���� ������ ���� �ִ� �õ� Ƚ��
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
        // ���� ��ֹ� ����
        foreach (var obstacle in spawnedObstacles)
        {
            if (obstacle != null)
                Destroy(obstacle);
        }

        // �� ��ֹ� ����
        SpawnObstacles();
    }
}