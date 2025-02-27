using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�� ������ ���� ���ӽ����̽�
//���������� ���Ͱ� ��� �׾����� ��Ż�� Ȱ��ȭ�ϴ� ��ũ��Ʈ
public class StageMaanager : MonoBehaviour
{
    public GameObject portal; //��Ż ������Ʈ (Inspector���� ����)
    private bool isPlay; //���� ��� Ȯ�ο�

    void Start()
    {
        portal.SetActive(false); //ó������ ��Ż ��Ȱ��ȭ
    }

    void Update()
    {
        if (AreAllEnemysDead()) //���Ͱ� ��� �׾�����
        {
            if (!isPlay) //���尡 ��������� ������
            {
                isPlay = true; //���� ��������� ����
                SoundManager.instance.PlaySound(SFX.StageClear); //�������� Ŭ���� ���� ���
            }
            portal.SetActive(true); // ���Ͱ� �� ������ ��Ż Ȱ��ȭ
        }
    }

    bool AreAllEnemysDead() //���Ͱ� ��� �׾����� Ȯ���ϴ� �Լ�
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0; //���� �±׸� ���� ������Ʈ�� ������ 0�̸� true ��ȯ
    }
}
