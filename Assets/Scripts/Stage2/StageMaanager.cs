using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMaanager : MonoBehaviour
{
    public GameObject portal; // 포탈 오브젝트 (Inspector에서 연결)
    private bool isPlay; // 사운드 재생 확인용

    void Start()
    {
        portal.SetActive(false); // 처음에는 포탈 비활성화
    }

    void Update()
    {
        if (AreAllEnemysDead())
        {
            if (!isPlay)
            {
                isPlay = true;
                SoundManager.instance.PlaySound(SFX.StageClear);
            }
            portal.SetActive(true); // 몬스터가 다 죽으면 포탈 활성화
        }
    }

    bool AreAllEnemysDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
