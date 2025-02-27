using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Stage 시작시 플에이어의 위치를 초기화하는 스크립트
public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //씬이 로드될때마다 OnSceneLoaded 함수 호출
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //씬이 로드될때마다 OnSceneLoaded 함수 호출
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //씬이 로드될때마다 호출되는 함수
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //플레이어 태그를 가진 오브젝트를 찾음
        if (player != null)
        {
            player.transform.position = new Vector3(-0.03f, -4.13f, 0); //플레이어의 위치를 초기화
        }
    }
}
