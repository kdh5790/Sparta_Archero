using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ��ֹ��� �����ϴ� ��ũ��Ʈ
public class TrapA : MonoBehaviour
{
    public GameObject obstaclePrefab; //��ֹ� ������
    public int minObstacles = 2;  //�ּ� ��ֹ� ����
    public int maxObstacles = 6; //�ִ� ��ֹ� ����
    public Vector2 spawnAreaMin = new Vector2(-2.4f, -4.4f);  //���� ���� �ּ� ��ǥ
    public Vector2 spawnAreaMax = new Vector2(2.4f, 3.5f);  //���� ���� �ִ� ��ǥ

    public Vector2 restrictedMin = new Vector2(-1f, -5f); //���� ���� ���� �ּ� ��ǥ
    public Vector2 restrictedMax = new Vector2(1f, -3.4f);  //���� ���� ���� �ִ� ��ǥ

    public float obstacleRadius = 0.5f; //��ֹ��� ������ (��ġ�� �ʰ� �ϱ� ���� ���)

    private List<GameObject> spawnedObstacles = new List<GameObject>(); //������ ��ֹ� ����Ʈ

    void Start()
    {
        SpawnObstacles(); //��ֹ� ����
    }

    void SpawnObstacles() //��ֹ� ���� �Լ�
    {
        int obstacleCount = Random.Range(minObstacles, maxObstacles + 1); //������ ��ֹ� ����
        spawnedObstacles.Clear(); //������ ��ֹ� ����Ʈ �ʱ�ȭ

        for (int i = 0; i < obstacleCount; i++) //������ ��ֹ� ������ŭ �ݺ�
        {
            Vector2 spawnPosition;
            int maxAttempts = 20; // ���� ���� ������ ���� �ִ� �õ� Ƚ��
            int attempts = 0;

            do
            {
                spawnPosition = new Vector2( //������ ��ġ ����
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x), //x��ǥ
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y) //y��ǥ
                );
                attempts++;
            }
            while ((IsInsideRestrictedArea(spawnPosition) || IsOverlapping(spawnPosition)) && attempts < maxAttempts); //���� ���� �����̰ų� ��ġ�� ���

            if (attempts < maxAttempts) //�ִ� �õ� Ƚ������ ������
            {
                GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity); //��ֹ� ����
                spawnedObstacles.Add(newObstacle); //������ ��ֹ� ����Ʈ�� �߰�
            }
        }
    }

    bool IsInsideRestrictedArea(Vector2 position) //���� ���� ������ �ִ��� Ȯ���ϴ� �Լ�
    {
        return position.x >= restrictedMin.x && position.x <= restrictedMax.x && //x��ǥ�� ���� ���� ���� �ִ���
               position.y >= restrictedMin.y && position.y <= restrictedMax.y; //y��ǥ�� ���� ���� ���� �ִ���
    }

    bool IsOverlapping(Vector2 position) //��ֹ��� ��ġ���� Ȯ���ϴ� �Լ�
    {
        return Physics2D.OverlapCircle(position, obstacleRadius) != null; //�־��� ��ġ�� ��������ŭ�� ���� ��ġ���� Ȯ��
    }

    public void ResetObstacles() //��ֹ� �ʱ�ȭ �Լ�
    {
        // ���� ��ֹ� ����
        foreach (var obstacle in spawnedObstacles) //������ ��ֹ� ����Ʈ���� �ݺ�
        {
            if (obstacle != null) //��ֹ��� �����Ҷ�
                Destroy(obstacle); //��ֹ� ����
        }

        SpawnObstacles(); //��ֹ� ����
    }
}