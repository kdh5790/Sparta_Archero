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

        // �±װ� "Enemy" �� ������Ʈ���� ã�Ƽ� List�� ��ȯ
        enemyList = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemy")).ToList();
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        FindTarget();

        if (target != null)
            LookAtTarget();

        if (!playerController.isMove && target != null && playerController.stopTime > 0.4f)
            animator.SetBool("IsAttack", true);

        else if (playerController.isMove || target == null)
            animator.SetBool("IsAttack", false);
    }

    // ���� ����� �� ã��
    private void FindTarget()
    {
        float targetDistance = 100f;

        foreach (GameObject obj in enemyList)
        {
            // ���� �÷��̾��� �Ÿ� ���ϱ�
            float cloneDistance = Vector3.Distance(obj.transform.position, transform.position);

            // ���� ����� �� ã��
            if (cloneDistance < targetDistance)
            {
                targetDistance = cloneDistance;
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
