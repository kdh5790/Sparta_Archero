using System.Collections;
using UnityEngine;
using Utils;  // TargetingUtils 네임스페이스

public abstract class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 3f;
    public Transform target;

    [Header("Damage Settings")]
    public float maxHealth = 100;
    protected float currentHealth;
    public int exp = 10;    //몬스터별 경험치 기본값
    public Animator enemyAnimator;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    protected bool isDead = false;
    public bool IsDead => isDead;

    protected bool isHeadShot = false;
    public bool IsHeadShot => isHeadShot;

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriter;

    private bool isDealingDamage = false;  // 현재 데미지를 주고 있는지 확인

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponentInChildren<SpriteRenderer>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        if (enemyAnimator == null)
            enemyAnimator = GetComponentInChildren<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        if (isDead || target == null) return;
        MoveTowardsTarget();
    }

    protected virtual void LateUpdate()
    {
        if (isDead || target == null) return;
        spriter.flipX = target.position.x < transform.position.x;
    }

    public virtual void MoveTowardsTarget()
    {
        Vector3 direction = TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }

    // 플레이어와 충돌 중일 때 일정 간격으로 데미지를 줌
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null && !isDealingDamage)
            {
                playerStats.OnDamaged(120);
            }
        }
    }


    // 몬스터가 데미지를 받음
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took damage: {damage}. Remaining health: {currentHealth}");

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Die");
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        GiveExpToPlayer();

        Destroy(gameObject, 2f);
    }


    //플레이어에게 경험치 주기
    private void GiveExpToPlayer()
    {
        if (target != null)
        {
            PlayerStats playerStats = target.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.IncreaseExp(exp);
            }
        }
    }
}
