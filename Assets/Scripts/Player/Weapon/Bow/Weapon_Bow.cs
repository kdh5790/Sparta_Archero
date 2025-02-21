using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_Bow : MonoBehaviour
{
    private int damage = 30;
    public int Damage { get { return damage; } set { damage = value; } }

    private float attackSpeed = 1f;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    private int criticalDamage = 150;
    public int CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }

    private int criticalChance = 0;
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    public List<GameObject> enemyList = new List<GameObject>(); // �ʵ��� ������ ���� ����Ʈ
    public GameObject target; // �����ؾ� �� Ÿ��

    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();

        playerController = GetComponentInParent<PlayerController>();

        UpdateEnemyList();
        FindTarget();
    }

    void Update()
    {
        if (playerController.isMove || target == null)
            FindTarget();

        LookAtTarget();

        // �÷��̾ �������� + Ÿ���� ���� + ���߰� �����ð��� ����
        if (!playerController.isMove && target != null && playerController.stopTime > 0.4f)
            animator.SetBool("IsAttack", true);
        else
            animator.SetBool("IsAttack", false);
    }

    // ���� Ȱ��ȭ �� ���� ã�ƿ���                 �±װ� "Enemy"�̰� Ȱ��ȭ �� ������ ã�� ����Ʈ�� ��ȯ
    public void UpdateEnemyList() => enemyList = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemy") && x.activeSelf).ToList();


    // ���� ����� �� ã��
    private void FindTarget()
    {
        if (target != null && !target.activeSelf || target == null)
            UpdateEnemyList();


        if (enemyList != null)
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
        else
        {
            target = null;
        }
    }

    // Ÿ�� �ٶ󺸱�
    private void LookAtTarget()
    {
        if (target == null) return;
        // �ٶ󺸴� ���� ���ϱ�
        Vector2 direction = target.transform.position - transform.position;

        // ���� ���� ��� �� ���� ���� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z ȸ�������� Ÿ�� �ٶ󺸱�
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Attack()
    {
        if (target != null)
            PlayerManager.instance.arrowManager.StartShootDelegate(target);
    }

    public void IncreasedAttackSpeed(float speed)
    {
        AttackSpeed += speed;
        animator.speed = AttackSpeed;
    }
}
