using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public GameObject target;

    private int damage; // ������
    private int bound = 0; // �ݵ� Ƚ��

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

        if (Vector3.Distance(transform.parent.position, transform.position) > maxDistance)
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
    }

    private void OnEnable()
    {
        bound = 0;
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
            {
                damage = (int)(Random.Range(bow.Damage * (bow.CriticalDamage - 0.1f), bow.Damage * (bow.CriticalDamage + 0.1f)));
                Debug.Log($"�� �浹 | ũ��Ƽ�� ������ : {damage}");
            }

            else
            {
                damage = (int)(Random.Range(bow.Damage * 0.9f, bow.Damage * 1.1f));
                Debug.Log($"�� �浹 | ������ : {damage}");
            }

            // �ݵ� ��ų ���� + ���� ƨ�� Ƚ���� 2���� �۴ٸ� ���� Ÿ�� ã�� �̵���Ŵ
            if (bow.IsRebound && bound < 2)
            {
                target = bow.FindTarget(transform, collision.gameObject);
                if (target != null)
                {
                    float angle = PlayerManager.instance.arrowManager.LookAtTargetForArrow(target, transform);
                    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                    bound++;

                    return;
                }
            }

            // ���뼦 ��ų�� �������� �ʾҴٸ� �������� ���� �� ȭ�� ��Ȱ��ȭ
            if (!bow.IsPiercingShot)
            {
                GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
                return;
            }
        }
    }
}
