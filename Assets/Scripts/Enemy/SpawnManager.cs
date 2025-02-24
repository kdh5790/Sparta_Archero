using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;    // ������ �� �����յ�
    public Transform[] spawnPoints;      // ���� ��ġ �迭
    public float spawnInterval = 5.0f;     // ���� ����

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
            return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
    }
}
