using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject holePrefab;
    public GameObject wallPrefab;

    //다음 스테이지 출입구 원활하게 통과를 위해 y좌표를 10보다 작게 설정
    private Vector3 dungeonSize = new Vector2(6, 8); 
    public float holeProbability = 0.5f; //구멍이 생성될 확률 반반 무 많이

    private void Start()
    {
        GenerateObstacles();
    }

    public void GenerateObstacles()
    {
        //장애물이 구멍이 나올 확률내라면
        if (Random.value < holeProbability)
        {
            //구멍이 생성될 경우 5개에서 10개 생성
            int numberOfHoles = Random.Range(5, 10);

            for (int i = 0; i < numberOfHoles; i++)
            {
                //랜덤 위치 지정
                Vector2 randomPosition =
                    new Vector2
                    (Random.Range(-dungeonSize.x / 2, dungeonSize.x / 2),
                    Random.Range(-dungeonSize.y / 2, dungeonSize.y / 2) );

                //지정된 위치에 구멍 생성
                Instantiate(holePrefab, randomPosition, Quaternion.identity);
            }
        }
        else //벽이 생성될 경우
        {
            //위치 랜덤 지정
            Vector2 randomPosition =
                new Vector2
                (Random.Range(-dungeonSize.x / 2, dungeonSize.x / 2),
                Random.Range(-dungeonSize.y / 2, dungeonSize.y / 2));

            //지정된 위치에 벽 생성
            Instantiate(wallPrefab, randomPosition, Quaternion.identity);
        }
    }
}
