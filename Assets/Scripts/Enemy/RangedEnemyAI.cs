using System.Collections;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour              //���Ÿ� �� AI
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
        player = GameObject.FindGameObjectWithTag("Player")?.transform; //�ʻ��� �÷��̾� �˻�

        // firePoint �ڵ� �Ҵ� (������ ã��)
        if (firePoint == null)
        {
            firePoint = transform.Find("firePoint");    //����ü ���� ����
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

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);     //���Ϳ� �÷��̾��� �Ÿ� ���

        if (distanceToPlayer <= attackRange)            //�Ÿ��� ���ݻ�Ÿ����� �������
        {
            enemyAI.enabled = false;        //�߰ݸ���
            AttackPlayer();                 //�÷��̾� ����
        }
        else
        {
            enemyAI.enabled = true;             //�Ÿ��� ���ݻ�Ÿ����� �ְ��
            enemyAI.MoveTowardsTarget();        //�÷��̾� �߰�
        }
    }


    void AttackPlayer()                     //�÷��̾� ����
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
                    Vector2 direction = (player.position - firePoint.position).normalized;          //���⼳��
                    rb.velocity = direction * projectileSpeed;                                      //�ش� ���� * ����ü�ӵ��� ����ü �����̱�

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
