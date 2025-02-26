using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject enemyPrfab;
    public GameObject obstaclePrefab;
    public Transform[] spawnPoints; //���� ��ֹ��� ������ ��ġ
    public int numberOfEnemies; //������ ���� ��
    public int numberOfObstacles;
    private int enemiesDefeated = 0; //óġ�� ���� ��

    private void Start()
    {
        GenerateStage();
    }
    
    public void GenerateStage()
    {
        //��ֹ� ����
        for(int i = 0; i < numberOfObstacles; i++)
        {

        }

        //�� ����
    }

    //���� ��ġ �Լ�

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        if (enemiesDefeated >= numberOfEnemies)
            OpenNextStageDoor();
    }

    public void OpenNextStageDoor()
    {
        //�� ���� ����
        //���� ���������� �Ѿ�� �ڵ�
        //SceneManager.LoadScene("NextStage");
    }
}
