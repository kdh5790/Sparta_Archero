// EnemyAI.cs
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public Transform player;              // �÷��̾� ��ġ
    public float moveSpeed = 3.0f;          // �̵� �ӵ�
    public float chaseDistance = 10.0f;     // ���� �Ÿ�
    public float attackDistance = 2.0f;     // ���� �Ÿ�
    public float attackCooldown = 1.0f;     // ���� ��Ÿ��

    protected float attackTimer = 0f;

    protected virtual void Start()
    {
        // "Player" �±װ� ���� ������Ʈ ã��
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        // EnemyManager�� �ڽ� ��� (���� EnemyManager�� �־�� ��)
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
            enemyManager.RegisterEnemy(this);
    }

    protected virtual void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= chaseDistance)
            ChasePlayer(distance);
    }

    protected virtual void ChasePlayer(float distance)
    {
        transform.LookAt(player);
        if (distance > attackDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown;
            }
        }

        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
    }

    protected virtual void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // �� ������ �ٸ��� ������ ���� ���
    protected abstract void Attack();


    private void OnDestroy()
    {
        // �ı��� �� EnemyManager���� �ڽ� ����
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
            enemyManager.UnregisterEnemy(this);
    }
}
