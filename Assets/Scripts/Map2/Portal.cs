using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string[] stageScenes; // �̵��� �������� �� ���

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ �浹�߰�, ��Ż ������Ʈ�� �浹�� ��츸 �� �̵�
        if (other.CompareTag("Player") && gameObject.CompareTag("Portal"))
        {
            string randomStage = stageScenes[Random.Range(0, stageScenes.Length)];
            SceneManager.LoadScene(randomStage);
        }
    }
}
