using System.Collections;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Ranged Enemy Settings")]
    public GameObject projectilePrefab;  // ����ü ������
    public Transform firePoint;         // ����ü �߻� ��ġ
    public float attackRange = 5f;      // ���� ����
    public float attackCooldown = 2f;   // ���� ��Ÿ��
    public float projectileSpeed = 5f;  // ����ü �ӵ�

    private BasicEnemyAI enemyAI;
    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        enemyAI = GetComponent<BasicEnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // firePoint �ڵ� �Ҵ� (������ ã��)
        if (firePoint == null)
        {
            firePoint = transform.Find("firePoint");
            if (firePoint == null)
            {
            }
            else
            {
            }
        }


        if (projectilePrefab == null)
        {
        }
    }



    void Update()
    {
        if (enemyAI == null || player == null || enemyAI.IsDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            enemyAI.enabled = false; // �̵� ����
            AttackPlayer();
        }
        else
        {
            enemyAI.enabled = true; // �ٽ� �̵� ����
            enemyAI.MoveTowardsTarget();
        }
    }


    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            Debug.Log("���Ÿ� ���� ����!");

            if (firePoint != null && projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Debug.Log($"����ü ������: {projectile.name}");

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 direction = (player.position - firePoint.position).normalized;
                    rb.velocity = direction * projectileSpeed;
                    Debug.Log($" ����ü ����: {direction}, �ӵ�: {rb.velocity}");
                }
                else
                {
                }

                Destroy(projectile, 5f);  // 5�� �� ����ü ����
            }
            else
            {
            }
        }
    }

}
