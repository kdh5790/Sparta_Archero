using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int openTreasure = Animator.StringToHash("OpenTreasure");

    protected Animator animator;
    public float interactionDistance = 1f; //���ڿ� ��ȣ�ۿ� �Ÿ�
    private bool isOpen = false; //ó�� ���� ���ȴ��� ����

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    { 

        //�÷��̾��� ��ġ�� ��������, �÷��̾ ���ڿ� �ٰ����� OpenTreasure�޼��尡 ����ǵ��� ��
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            //Debug.Log($"{distance}");

            if (distance < interactionDistance && !isOpen)
                OpenTreasure();

        }
    }

    public void OpenTreasure()
    {
        if(isOpen == false)
        {
            PlayerManager.instance.stats.BoxOpen += 1; //���� ������
            DataManager.Instance.SaveBoxOpen(PlayerManager.instance.stats.BoxOpen); //����������
            UIManager.Instance.ChestTriggered();
        }

        //Debug.Log("����");
        animator.SetBool(openTreasure, true);
        isOpen = true;
    }
}
