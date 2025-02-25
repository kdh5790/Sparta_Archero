using UnityEngine;
using Utils;  // TargetingUtils�� �ִ� ���ӽ����̽�

public abstract class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 3f;
    public Transform target; // �⺻ Ÿ�� (���� �÷��̾�)

    [Header("Damage Settings")]
    public float maxHealth = 100;
    protected float currentHealth;
    public Animator enemyAnimator; // Hit, Die �ִϸ��̼� Ʈ���ſ�


    
    public float CurrentHealth => currentHealth;    // ü�� �� �̰� �������ø� �˴ϴ�!
    public float MaxHealth => maxHealth;    // ü�� �� �̰� �������ø� �˴ϴ�!

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

    // �ǰ� �� ȣ��Ǵ� �޼���
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took damage: {damage}. Remaining health: {currentHealth}");

        // ���� �÷��� �ִϸ��̼� ��� (�ǰ� ȿ��)
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ��� ó�� �޼���
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        // ���������� ��� �ִϸ��̼� ���
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Die");
        }

        // �߰� ���߱�: FixedUpdate���� isDead üũ�� ó����

        // �߰� �浹�� ���� �������� �� ���� �ʵ��� Collider ��Ȱ��ȭ
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // ��� �� ���� �ð� �� ������Ʈ ���� (���� ����)
        Destroy(gameObject, 2f);
    }
}
