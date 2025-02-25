using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMaanager : MonoBehaviour
{
    public GameObject portal; // 포탈 오브젝트 (Inspector에서 연결)

    void Start()
    {
        portal.SetActive(true); // 처음에는 포탈 비활성화 test 끝나면 false로 바꾸기
    }

    void Update()
    {
        if (AreAllMonstersDead())
        {
            portal.SetActive(false); // 몬스터가 다 죽으면 포탈 활성화 test 끝나면 true로 바꾸기
        }
    }

    bool AreAllMonstersDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
