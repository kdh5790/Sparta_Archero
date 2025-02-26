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
    public int exp = 10;    // 몬스터별 경험치 기본값
    public Animator enemyAnimator;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    protected bool isDead = false;
    public bool IsDead => isDead;

    protected bool isHeadShot = false;
    public bool IsHeadShot { get { return isHeadShot; } set { isHeadShot = value; } }

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriter;

    private bool isDealingDamage = false;  // 현재 데미지를 주고 있는지 확인

    public bool isBoss; //유니티나 매서드에서 할당
    public bool isFinalBoss; //유니티나 매서드에서 할당

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

        if(isBoss) //만약 해당 몬스터가 보스 몬스터라면
        {
            UIManager.Instance.GetBossReward(); //보스 보상을 주세요
        }

        if(isFinalBoss) //만약 해당 몬스터가 던전 마지막 몬스터라면
        {
            UIManager.Instance.CallDungeonClear(PlayerManager.instance.stats.ClearTime,
                PlayerManager.instance.stats.Level);//클리어 화면을 보여주세요
        }

        Destroy(gameObject, 2f);
    }

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
