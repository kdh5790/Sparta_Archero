using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utils;  // TargetingUtils 네임스페이스

public abstract class EnemyAI : MonoBehaviour   //몬스터AI 메인 스크립트
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

    NavMeshAgent agent; // 장애물 피해 길찾기



    protected virtual void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponentInChildren<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");


        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {

            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats != null)
            {
                target = playerStats.GetComponent<Transform>();
            }
        }


    }

    protected virtual void Start()
    {

        currentHealth = maxHealth;
        if (enemyAnimator == null)
            enemyAnimator = GetComponentInChildren<Animator>();

        if(gameObject.name.Contains("Chort"))           //이 몬스터의 이름이 "Chort" 일경우 길찾기 수행
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false; // 길찾기 수행시 캐릭터 회전 금지
            agent.updateUpAxis = false; // 길찾기 수행환경을 2D로 제한
        }
    }

    protected virtual void Update()
    {
        if (isDead || target == null) return;
        if (agent != null && agent.isOnNavMesh && target != null && gameObject.name.Contains("Chort"))
        {
            agent.SetDestination(target.position);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isDead || target == null) return;
        if (!gameObject.name.Contains("Chort"))
         MoveTowardsTarget();
    }

    protected virtual void LateUpdate()
    {
        if (isDead || target == null) return;
        spriter.flipX = target.position.x < transform.position.x;
    }

    public virtual void MoveTowardsTarget()         //우회없는 기본 추적
    {
        Vector3 direction = TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }

    private void OnTriggerStay2D(Collider2D collision)  //몬스터와 플레이어가 충돌하면
    {
        if (!isDead && collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null && !isDealingDamage)
            {
                playerStats.OnDamaged(120);                 //플레이어에게 데미지
            }
        }
    }

    public virtual void TakeDamage(int damage)              //몬스터가 데미지를 받음
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took damage: {damage}. Remaining health: {currentHealth}");

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Hit");            //피격 애니메이션 실행
        }

        if (currentHealth <= 0)         //체력이 0이하면 사망처리
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
            enemyAnimator.SetTrigger("Die");            //사망 애니메이션 실행
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        GiveExpToPlayer();

        if (isBoss) //만약 해당 몬스터가 보스 몬스터라면
        {
            UIManager.Instance.GetBossReward(); //보스 보상을 주세요
        }

        if (isFinalBoss) //만약 해당 몬스터가 던전 마지막 몬스터라면
        {
            UIManager.Instance.CallDungeonClear(PlayerManager.instance.stats.ClearTime,
                PlayerManager.instance.stats.Level);//클리어 화면을 보여주세요
        }

        Destroy(gameObject, 0.3f);
    }

    private void GiveExpToPlayer()  //플레이어에게 경험치 지급
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