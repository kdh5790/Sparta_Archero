using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Stage ���۽� �ÿ��̾��� ��ġ�� �ʱ�ȭ�ϴ� ��ũ��Ʈ
public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //���� �ε�ɶ����� OnSceneLoaded �Լ� ȣ��
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //���� �ε�ɶ����� OnSceneLoaded �Լ� ȣ��
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //���� �ε�ɶ����� ȣ��Ǵ� �Լ�
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� �±׸� ���� ������Ʈ�� ã��
        if (player != null)
        {
            player.transform.position = new Vector3(-0.03f, -4.13f, 0); //�÷��̾��� ��ġ�� �ʱ�ȭ
        }
    }
}
