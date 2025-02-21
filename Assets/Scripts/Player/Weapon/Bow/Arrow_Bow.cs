using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bow : MonoBehaviour
{
    private int damage = 30;
    public int Damage { get { return damage; } set { damage = value; } }

    private float attackSpeed = 1f;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    private int criticalDamage = 150;
    public int CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }

    private int criticalChance = 0;
    public int CriticalCance { get { return criticalChance; } set { criticalChance = value; } }


    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public GameObject target;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bow = FindObjectOfType<Weapon_Bow>();
    }

    private void OnEnable()
    {
        if (bow.target != null)
        {
            target = bow.target;
            LookAtTarget();
        }
    }

    private void Update()
    {
        MoveToTarget();

        if (Vector3.Distance(transform.parent.position, transform.position) > 12f)
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
    }

    private void MoveToTarget()
    {
        if(target != null)
            rigidBody.velocity = transform.up * 3f;
    }

    private void LookAtTarget()
    {
        // �ٶ󺸴� ���� ���ϱ�
        Vector2 direction = target.transform.position - transform.position;

        // ���� ���� ��� �� ���� ���� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z ȸ�������� Ÿ�� �ٶ󺸱�
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.enabled) return;

        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("�� �浹");
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
        }
    }


}
