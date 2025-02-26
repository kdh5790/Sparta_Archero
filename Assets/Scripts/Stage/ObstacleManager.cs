using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject holePrefab;
    public GameObject wallPrefab;

    //���� �������� ���Ա� ��Ȱ�ϰ� ����� ���� y��ǥ�� 10���� �۰� ����
    private Vector3 dungeonSize = new Vector2(6, 8); 
    public float holeProbability = 0.5f; //������ ������ Ȯ�� �ݹ� �� ����

    private void Start()
    {
        GenerateObstacles();
    }

    public void GenerateObstacles()
    {
        //��ֹ��� ������ ���� Ȯ�������
        if (Random.value < holeProbability)
        {
            //������ ������ ��� 5������ 10�� ����
            int numberOfHoles = Random.Range(5, 10);

            for (int i = 0; i < numberOfHoles; i++)
            {
                //���� ��ġ ����
                Vector2 randomPosition =
                    new Vector2
                    (Random.Range(-dungeonSize.x / 2, dungeonSize.x / 2),
                    Random.Range(-dungeonSize.y / 2, dungeonSize.y / 2) );

                //������ ��ġ�� ���� ����
                Instantiate(holePrefab, randomPosition, Quaternion.identity);
            }
        }
        else //���� ������ ���
        {
            //��ġ ���� ����
            Vector2 randomPosition =
                new Vector2
                (Random.Range(-dungeonSize.x / 2, dungeonSize.x / 2),
                Random.Range(-dungeonSize.y / 2, dungeonSize.y / 2));

            //������ ��ġ�� �� ����
            Instantiate(wallPrefab, randomPosition, Quaternion.identity);
        }
    }
}
