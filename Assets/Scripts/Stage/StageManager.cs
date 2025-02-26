using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject enemyPrfab;
    public GameObject obstaclePrefab;
    public Transform[] spawnPoints; //적과 장애물이 생성될 위치
    public int numberOfEnemies; //생성할 적의 수
    public int numberOfObstacles;
    private int enemiesDefeated = 0; //처치한 적의 수

    private void Start()
    {
        GenerateStage();
    }
    
    public void GenerateStage()
    {
        //장애물 생성
        for(int i = 0; i < numberOfObstacles; i++)
        {

        }

        //적 생성
    }

    //랜덤 위치 함수

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        if (enemiesDefeated >= numberOfEnemies)
            OpenNextStageDoor();
    }

    public void OpenNextStageDoor()
    {
        //문 여는 로직
        //다음 스테이지로 넘어가는 코드
        //SceneManager.LoadScene("NextStage");
    }
}
