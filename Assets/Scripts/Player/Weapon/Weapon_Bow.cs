using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_Bow : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>(); // �ʵ��� ������ ���� ����Ʈ
    public GameObject target; // �����ؾ� �� Ÿ��

    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();

        playerController = GetComponentInParent<PlayerController>();

        UpdateEnemyList();
    }

    void Update()
    {
        if (playerController.isMove)
            FindTarget();

        if (target != null)
            LookAtTarget();

        // �÷��̾ �������� + Ÿ���� ���� + ���߰� �����ð��� ����
        if (!playerController.isMove && target != null && playerController.stopTime > 0.4f)
            animator.SetBool("IsAttack", true);
        else
            animator.SetBool("IsAttack", false);
    }

    // ���� Ȱ��ȭ �� ���� ã�ƿ���                 �±װ� "Enemy"�� ������ ã�� ����Ʈ�� ��ȯ
    public void UpdateEnemyList() => enemyList = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemy")).ToList();


    // ���� ����� �� ã��
    private void FindTarget()
    {
        float targetDistance = 100f;

        foreach (GameObject obj in enemyList)
        {
            // ���� �÷��̾��� �Ÿ� ���ϱ�
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            // ���� ����� �� ã��
            if (distance < targetDistance)
            {
                targetDistance = distance;
                target = obj;
            }
        }

    }

    // Ÿ�� �ٶ󺸱�
    private void LookAtTarget()
    {
        // �ٶ󺸴� ���� ���ϱ�
        Vector2 direction = target.transform.position - transform.position;

        // ���� ���� ��� �� ���� ���� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z ȸ�������� Ÿ�� �ٶ󺸱�
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
