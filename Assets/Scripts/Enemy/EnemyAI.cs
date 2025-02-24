// EnemyAI.cs
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public Transform player;              // 플레이어 위치
    public float moveSpeed = 3.0f;          // 이동 속도
    public float chaseDistance = 10.0f;     // 추적 거리
    public float attackDistance = 2.0f;     // 공격 거리
    public float attackCooldown = 1.0f;     // 공격 쿨타임

    protected float attackTimer = 0f;

    protected virtual void Start()
    {
        // "Player" 태그가 붙은 오브젝트 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        // EnemyManager에 자신 등록 (씬에 EnemyManager가 있어야 함)
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

    // 각 적마다 다르게 구현할 공격 방식
    protected abstract void Attack();


    private void OnDestroy()
    {
        // 파괴될 때 EnemyManager에서 자신 제거
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
            enemyManager.UnregisterEnemy(this);
    }
}
