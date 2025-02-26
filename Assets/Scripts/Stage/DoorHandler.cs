using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public Animator DoorAnimator;
    public GameObject[] stageEnemys;//�� ���� �� ������Ʈ �迭
    private bool allEnemysDeath = false; //��� ���� óġ ����

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
