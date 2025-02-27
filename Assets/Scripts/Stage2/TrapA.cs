using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 장애물을 생성하는 스크립트
public class TrapA : MonoBehaviour
{
    public GameObject obstaclePrefab; //장애물 프리팹
    public int minObstacles = 2;  //최소 장애물 개수
    public int maxObstacles = 6; //최대 장애물 개수
    public Vector2 spawnAreaMin = new Vector2(-2.4f, -4.4f);  //스폰 영역 최소 좌표
    public Vector2 spawnAreaMax = new Vector2(2.4f, 3.5f);  //스폰 영역 최대 좌표

    public Vector2 restrictedMin = new Vector2(-1f, -5f); //생성 금지 영역 최소 좌표
    public Vector2 restrictedMax = new Vector2(1f, -3.4f);  //생성 금지 영역 최대 좌표

    public float obstacleRadius = 0.5f; //장애물의 반지름 (겹치지 않게 하기 위해 사용)

    private List<GameObject> spawnedObstacles = new List<GameObject>(); //생성된 장애물 리스트

    void Start()
    {
        SpawnObstacles(); //장애물 생성
    }

    void SpawnObstacles() //장애물 생성 함수
    {
        int obstacleCount = Random.Range(minObstacles, maxObstacles + 1); //랜덤한 장애물 개수
        spawnedObstacles.Clear(); //생성된 장애물 리스트 초기화

        for (int i = 0; i < obstacleCount; i++) //랜덤한 장애물 개수만큼 반복
        {
            Vector2 spawnPosition;
            int maxAttempts = 20; // 무한 루프 방지를 위한 최대 시도 횟수
            int attempts = 0;

            do
            {
                spawnPosition = new Vector2( //랜덤한 위치 생성
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x), //x좌표
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y) //y좌표
                );
                attempts++;
            }
            while ((IsInsideRestrictedArea(spawnPosition) || IsOverlapping(spawnPosition)) && attempts < maxAttempts); //생성 금지 영역이거나 겹치는 경우

            if (attempts < maxAttempts) //최대 시도 횟수보다 작을때
            {
                GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity); //장애물 생성
                spawnedObstacles.Add(newObstacle); //생성된 장애물 리스트에 추가
            }
        }
    }

    bool IsInsideRestrictedArea(Vector2 position) //생성 금지 영역에 있는지 확인하는 함수
    {
        return position.x >= restrictedMin.x && position.x <= restrictedMax.x && //x좌표가 제한 범위 내에 있는지
               position.y >= restrictedMin.y && position.y <= restrictedMax.y; //y좌표가 제한 범위 내에 있는지
    }

    bool IsOverlapping(Vector2 position) //장애물이 겹치는지 확인하는 함수
    {
        return Physics2D.OverlapCircle(position, obstacleRadius) != null; //주어진 위치에 반지름만큼의 원이 겹치는지 확인
    }

    public void ResetObstacles() //장애물 초기화 함수
    {
        // 기존 장애물 제거
        foreach (var obstacle in spawnedObstacles) //생성된 장애물 리스트에서 반복
        {
            if (obstacle != null) //장애물이 존재할때
                Destroy(obstacle); //장애물 제거
        }

        SpawnObstacles(); //장애물 생성
    }
}