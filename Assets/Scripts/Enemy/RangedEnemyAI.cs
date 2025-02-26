using System.Collections;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Ranged Enemy Settings")]
    public GameObject projectilePrefab;  // 투사체 프리팹
    public Transform firePoint;         // 투사체 발사 위치
    public float attackRange = 5f;      // 공격 범위
    public float attackCooldown = 2f;   // 공격 쿨타임
    public float projectileSpeed = 5f;  // 투사체 속도

    private BasicEnemyAI enemyAI;
    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        enemyAI = GetComponent<BasicEnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // firePoint 자동 할당 (없으면 찾기)
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
            enemyAI.enabled = false; // 이동 멈춤
            AttackPlayer();
        }
        else
        {
            enemyAI.enabled = true; // 다시 이동 가능
            enemyAI.MoveTowardsTarget();
        }
    }


    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            Debug.Log("원거리 공격 실행!");

            if (firePoint != null && projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Debug.Log($"투사체 생성됨: {projectile.name}");

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 direction = (player.position - firePoint.position).normalized;
                    rb.velocity = direction * projectileSpeed;
                    Debug.Log($" 투사체 방향: {direction}, 속도: {rb.velocity}");
                }
                else
                {
                }

                Destroy(projectile, 5f);  // 5초 후 투사체 제거
            }
            else
            {
            }
        }
    }

}
