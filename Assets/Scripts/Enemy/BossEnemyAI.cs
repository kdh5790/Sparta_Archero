using System.Collections;
using UnityEngine;

public class BossEnemyAI : MonoBehaviour
{
    private BasicEnemyAI enemyAI;
    private Transform player;

    [Header("충격파 패턴")]
    public float shockwaveCooldown = 5f; // 충격파 쿨타임
    private float lastShockwaveTime = 0f;
    public float shockwaveRadius = 3f; // 충격파 범위
    public int shockwaveDamage = 50; // 충격파 데미지
    

    public Animator shockwaveAnimator;

    [Header("분노 패턴")]
    private bool isEnraged = false;

    [Header("무적 패턴")]
    private bool isInvincible = false;
    public float invincibleDuration = 3f; // 무적 지속 시간

    void Start()
    {
        enemyAI = GetComponent<BasicEnemyAI>();


       if (shockwaveAnimator == null)
        {
            Transform shockwaveTransform = transform.Find("Shockwave");
            if (shockwaveTransform != null)
            {
                shockwaveAnimator = shockwaveTransform.GetComponent<Animator>();
            }
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (enemyAI == null || player == null || enemyAI.IsDead) return;

        // 충격파 패턴 발동
        if (Time.time - lastShockwaveTime >= shockwaveCooldown)
        {
            lastShockwaveTime = Time.time;
            StartCoroutine(CastShockwave());
        }

        // 체력이 30% 이하일 때 분노 모드
        if (!isEnraged && enemyAI.CurrentHealth <= enemyAI.MaxHealth * 0.3f)
        {
            isEnraged = true;
            enemyAI.instanceSpeed += 1f;
            Debug.Log("보스가 분노 상태. 속도 증가");
        }
    }

    // 충격파 공격 패턴
    IEnumerator CastShockwave()
    {
        Debug.Log("보스가 충격파를 사용");
        shockwaveAnimator.SetTrigger("Shockwave"); // 애니메이션 트리거



        yield return new WaitForSeconds(0.5f); // 충격파 애니메이션이 끝날 시간

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius);
        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.OnDamaged(shockwaveDamage);
                }
            }
        }
    }

    // 무적 패턴 (체력 20% 이하일 때 발동)
    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("보스가 무적 상태라 피해를 받지 않음");
            return;
        }

        enemyAI.TakeDamage(damage);

        if (enemyAI.CurrentHealth <= enemyAI.MaxHealth * 0.2f && !isInvincible)
        {
            StartCoroutine(ActivateInvincibility());
        }
    }

    IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        Debug.Log("보스 무적 상태");

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        Debug.Log("보스 무적 상태 해제");
    }
}
