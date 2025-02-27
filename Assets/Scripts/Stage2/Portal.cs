using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬 관리를 위한 네임스페이스
//플레이어가 포탈에 충돌했을때 다음 스테이지로 이동하는 스크립트
public class Portal : MonoBehaviour
{
    private string[] stageOrder = { //스테이지 순서
        "Stage_1", "Stage_2", "Stage_3", "Stage_4",
        "Stage_5","Stage_6", "Stage_7", "Stage_8", "Stage_9",
        "Stage_Boss", 
        "Stage_11", "Stage_12", "Stage_13", "Stage_14",
        "Stage_15", "Stage_16", "Stage_17", "Stage_18", "Stage_19",
        "Stage_Last Boss"
    };

    private void OnTriggerEnter2D(Collider2D other) //플레이어가 포탈에 충돌했을때 호출되는 함수
    {
        if (other.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name; //현재 씬 이름
            int index = System.Array.IndexOf(stageOrder, currentScene); //현재 씬의 인덱스

            if (index != -1 && index < stageOrder.Length - 1) //현재 씬이 스테이지 순서에 있고 마지막 스테이지가 아닐때
            {
                SceneManager.LoadScene(stageOrder[index + 1]); //다음 스테이지 로드
            }
        }
    }
}
