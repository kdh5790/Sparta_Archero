using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMaanager : MonoBehaviour
{
    public GameObject portal; // 포탈 오브젝트 (Inspector에서 연결)

    void Start()
    {
        portal.SetActive(false); // 처음에는 포탈 비활성화
    }

    void Update()
    {
        if (AreAllEnemysDead())
        {
            portal.SetActive(true); // 몬스터가 다 죽으면 포탈 활성화
        }
    }

    bool AreAllEnemysDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
