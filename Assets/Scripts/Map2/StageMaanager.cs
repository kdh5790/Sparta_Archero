using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMaanager : MonoBehaviour
{
    public GameObject portal; // ��Ż ������Ʈ (Inspector���� ����)

    void Start()
    {
        portal.SetActive(true); // ó������ ��Ż ��Ȱ��ȭ test ������ false�� �ٲٱ�
    }

    void Update()
    {
        if (AreAllEnemysDead())
        {
            portal.SetActive(false); // ���Ͱ� �� ������ ��Ż Ȱ��ȭ test ������ true�� �ٲٱ�
        }
    }

    bool AreAllEnemysDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
