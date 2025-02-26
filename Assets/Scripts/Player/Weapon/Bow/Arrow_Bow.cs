using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public BasicEnemyAI target;
    public int DamageValue { get { return damage; } }   //enemy�ʿ��� ������ �� �����Ϸ��� �־����ϴ�

    private int damage; // ������
    private int bound = 0; // �ݵ� Ƚ��
    private bool isPiercing = false; // ���� ��ų ���� �� ���� Ȯ�ο�

    private const float arrowSpeed = 12f; // ȭ�� �ӵ�
    private const float maxDistance = 15f; // �÷��̾�� ȭ���� �ִ� �Ÿ�(�ִ� �Ÿ��� �Ѿ�� ȭ�� ��Ȱ��ȭ)

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bow = FindObjectOfType<Weapon_Bow>();
    }

    private void Update()
    {
        MoveToTarget();

        // �÷��̾�� ���� �Ÿ� �̻� �־����ٸ� ȭ�� ȸ��(�����Ÿ��� �� ũ�⺸�� ũ�� ����)
        if (Vector3.Distance(transform.parent.position, transform.position) > maxDistance)
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
    }

    private void OnEnable()
    {
        bound = 0;
        isPiercing = false;

        if (bow != null) target = bow.target;
    }

    // Ÿ�� �������� �̵�
    private void MoveToTarget()
    {
        rigidBody.velocity = transform.up * arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ũ��Ƽ�� ���� Ȯ��
            bool isCritical = bow.CalculateCriticalChance();

            // ũ��Ƽ�� or �Ϲ� ������ �ֱ�
            if (isCritical)
                damage = (int)(bow.Damage * bow.CriticalDamage);

            else
                damage = bow.Damage;

            if (bow.IsRage)
                CalculateRageDamage();

            // �ݵ� Ƚ���� ���� ������ ����(�ִ� 2)
            if (bound > 0)
            {
                for (int i = 0; i < bound; i++)
                {
                    damage = (int)(damage * 0.7f);
                }
            }

            // ������ ���� ���� �ߴٸ� ������ ����
            if (isPiercing)
                damage = (int)(damage * 0.67f);

            // ��Ƽ�� ��ų�� �������̶�� ���� ������ 10% ����
            if (bow.IsMultiShot)
                damage = (int)(damage * 0.9f);

            Debug.Log(isCritical ? $"�� �浹 | ũ��Ƽ�� ������ : {damage}" : $"�� �浹 | ������ : {damage}");

            BasicEnemyAI enemy = collision.GetComponent<BasicEnemyAI>();

            if (enemy != null)
                enemy.TakeDamage(damage);

            // �ݵ� ��ų ���� + ���� ƨ�� Ƚ���� 2���� �۴ٸ� ���� Ÿ�� ã�� �̵���Ŵ
            if (bow.IsRebound && bound < 2)
            {
                target = bow.FindTarget(transform, collision.gameObject);
                if (target != null)
                {
                    bow.KnockBackEnemy(collision.transform, transform.position);

                    float angle = PlayerManager.instance.arrowManager.LookAtTargetForArrow(target.gameObject, transform);
                    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                    bound++;

                    return;
                }
            }

            // ���뼦 ��ų�� �������� �ʾҴٸ� �������� ���� �� ȭ�� ��Ȱ��ȭ
            if (!bow.IsPiercingShot)
            {
                bow.KnockBackEnemy(collision.transform, transform.position);
                GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
                return;
            }

            isPiercing = true;
        }
    }

    // �г� ��ų ���� �� ������ ���
    private void CalculateRageDamage()
    {
        float currentHP = PlayerManager.instance.stats.CurrentHealth;
        float maxHP = PlayerManager.instance.stats.MaxHealth;

        float percentage = (maxHP - currentHP) / maxHP * 100f;
        percentage = 1f + (percentage * 0.012f);

        damage = (int)(damage * percentage);
    }


}