using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMaanager : MonoBehaviour
{
    public GameObject portal; // ��Ż ������Ʈ (Inspector���� ����)

    void Start()
    {
        portal.SetActive(false); // ó������ ��Ż ��Ȱ��ȭ
    }

    void Update()
    {
        if (AreAllEnemysDead())
        {
            portal.SetActive(true); // ���Ͱ� �� ������ ��Ż Ȱ��ȭ
        }
    }

    bool AreAllEnemysDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
