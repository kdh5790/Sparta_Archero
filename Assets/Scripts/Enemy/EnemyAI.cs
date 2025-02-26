using UnityEngine;
using Utils;  // TargetingUtils가 있는 네임스페이스

public abstract class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 3f;
    public Transform target; // 기본 타겟 (보통 플레이어)

    [Header("Damage Settings")]
    public float maxHealth = 100;
    protected float currentHealth;
    public Animator enemyAnimator; // Hit, Die 애니메이션 트리거용


    
    public float CurrentHealth => currentHealth;    // 체력 값 이거 가져가시면 됩니다!
    public float MaxHealth => maxHealth;    // 체력 값 이거 가져가시면 됩니다!

    protected bool isDead = false;
    public bool IsDead { get { return isDead; } }

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriter;

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

    // 피격 시 호출되는 메서드
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took damage: {damage}. Remaining health: {currentHealth}");

        // 빨간 플래시 애니메이션 재생 (피격 효과)
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 사망 처리 메서드
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        // 투명해지는 사망 애니메이션 재생
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Die");
        }

        // 추격 멈추기: FixedUpdate에서 isDead 체크로 처리됨

        // 추가 충돌에 의해 데미지가 더 들어가지 않도록 Collider 비활성화
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 사망 후 일정 시간 후 오브젝트 제거 (선택 사항)
        Destroy(gameObject, 2f);
    }
}
