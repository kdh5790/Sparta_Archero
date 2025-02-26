using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public Animator DoorAnimator;
    public GameObject[] stageEnemys;//맵 상의 적 오브젝트 배열
    private bool allEnemysDeath = false; //모든 몬스터 처치 유무

    private void Update()
    {
        if (!allEnemysDeath)
            CheckEnemy();
    }

    public void CheckEnemy()
    {
        allEnemysDeath=true;
        foreach(GameObject Enemy in stageEnemys)
        {
            if (Enemy != null)
            {
                allEnemysDeath = false;
                break;
            }
        }

        if (allEnemysDeath)
            DoorAnimator.SetBool("OpenDoor", true);
    }
}
