using System.Collections;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour              //원거리 적 AI
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
        player = GameObject.FindGameObjectWithTag("Player")?.transform; //맵상의 플레이어 검색

        // firePoint 자동 할당 (없으면 찾기)
        if (firePoint == null)
        {
            firePoint = transform.Find("firePoint");    //투사체 생성 지점
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

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);     //몬스터와 플레이어의 거리 계산

        if (distanceToPlayer <= attackRange)            //거리가 공격사거리보다 가까울경우
        {
            enemyAI.enabled = false;        //추격멈춤
            AttackPlayer();                 //플레이어 공격
        }
        else
        {
            enemyAI.enabled = true;             //거리가 공격사거리보다 멀경우
            enemyAI.MoveTowardsTarget();        //플레이어 추격
        }
    }


    void AttackPlayer()                     //플레이어 공격
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;


            if (firePoint != null && projectilePrefab != null)
            {
                SoundManager.instance.PlaySound(SFX.RangeEnemyAttack);

                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);


                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 direction = (player.position - firePoint.position).normalized;          //방향설정
                    rb.velocity = direction * projectileSpeed;                                      //해당 방향 * 투사체속도로 투사체 움직이기

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
