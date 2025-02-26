using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Weapon_Bow : MonoBehaviour
{
    private int damage = 200; // 데미지
    public int Damage { get { return damage; } set { damage = value; } }

    private float attackSpeed = 1f; // 공격속도(애니메이션 속도로 조절)
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    private float criticalDamage = 2f; // 크리티컬 데미지
    public float CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }

    private int criticalChance = 10; // 크리티컬 확률
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    private bool isPiercingShot = false; // 관통 스킬 보유 여부
    public bool IsPiercingShot { get { return isPiercingShot; } set { isPiercingShot = value; } }

    private bool isRebound = false; // 반동 스킬 보유 여부
    public bool IsRebound { get { return isRebound; } set { isRebound = value; } }

    private bool isMultiShot = false; // 멀티샷 스킬 보유 여부
    public bool IsMultiShot { get { return isMultiShot; } set { isMultiShot = value; } }

    private bool isRage = false; // 분노 스킬 보유 여부
    public bool IsRage { get { return isRage; } set { isRage = value; } }

    private bool isHeadShot = false; // 헤드샷 스킬 보유 여부
    public bool IsHeadShot { get { return isHeadShot; } set { isHeadShot = value; } }


    private const float KnockBackPower = 1f; // 넉백 시 가할 힘

    public List<BasicEnemyAI> enemyList = new List<BasicEnemyAI>(); // 필드의 적들을 담을 리스트
    public BasicEnemyAI target; // 공격해야 할 타겟

    [SerializeField] private SpriteRenderer attakSpeedAuroraSprite;
    [SerializeField] private SpriteRenderer criticalAuroraSprite;
    private Animator animator;
    private PlayerController playerController;

    private IEnumerator[] skillCoroutineArr = new IEnumerator[2];

    void Start()
    {
        animator = GetComponent<Animator>();

        playerController = GetComponentInParent<PlayerController>();

        StartCoroutine(FindFirstTarget());
    }

    void Update()
    {
        if (playerController.isMove || (target != null && target.IsDead))
            target = FindTarget();

        LookAtTarget();

        // 플레이어가 멈춰있음 + 타겟이 있음 + 멈추고 일정시간이 지남
        if (!playerController.isMove && target != null && playerController.stopTime > 0.2f && !PlayerManager.instance.isDead)
            animator.SetBool("IsAttack", true);
        else
            animator.SetBool("IsAttack", false);
    }

    // 현재 활성화 된 적들 찾아오기
    public void UpdateEnemyList() => enemyList = FindObjectsOfType<BasicEnemyAI>().Where(x => x.CompareTag("Enemy") && !x.IsDead).ToList();


    // 가장 가까운 적 찾기
    public BasicEnemyAI FindTarget(Transform _transform = null, GameObject _enemy = null)
    {
        UpdateEnemyList();

        BasicEnemyAI go = null;

        float targetDistance = 100f;

        foreach (BasicEnemyAI obj in enemyList)
        {
            // 화살의 다음타겟을 찾을 경우 현재 자신(화살)의 타겟은 넘어가기
            if (_enemy != null && obj.name == _enemy.name || obj.IsDead)
                continue;

            float distance = 0;

            // 플레이어와 가장 가까운 적 찾기
            if (_transform == null)
                distance = Vector3.Distance(obj.transform.position, transform.position);

            // 반동 스킬 보유 시 화살의 다음 타겟 찾기
            else
                distance = Vector3.Distance(obj.transform.position, _transform.position);

            if (distance < targetDistance)
            {
                targetDistance = distance;
                go = obj;
            }
        }

        return go;
    }

    // 타겟 바라보기
    private void LookAtTarget()
    {
        if (target == null) return;
        // 바라보는 방향 구하기
        Vector2 direction = target.transform.position - transform.position;

        // 방향 각도 계산 후 라디안 값을 도 단위로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z 회전값으로 타겟 바라보기
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 공격(애니메이션 이벤트로 호출)
    private void Attack()
    {
        if (target != null)
            PlayerManager.instance.arrowManager.StartShootDelegate(target.gameObject);
    }

    // 공격속도 증가
    public void IncreasedAttackSpeed(float speed)
    {
        AttackSpeed += speed;
        animator.speed = AttackSpeed;
    }

    // 크리티컬 확인
    public bool CalculateCriticalChance()
    {
        int randNum = UnityEngine.Random.Range(0, 100);

        return randNum < criticalChance;
    }

    // 넉백 코루틴 시작(화살 스크립트에서 호출)
    public void KnockBackEnemy(Transform enemy, Vector3 arrowPos)
    {
        StartCoroutine(ApplyKnockBackCoroutine(enemy, arrowPos));
    }

    // 화살에 적중한 적 넉백
    public IEnumerator ApplyKnockBackCoroutine(Transform enemy, Vector3 arrowPos)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        enemyRb.bodyType = RigidbodyType2D.Dynamic;
        enemyRb.gravityScale = 0;
        Vector2 direction = (enemy.transform.position - arrowPos).normalized;

        enemyRb.AddForce(direction * KnockBackPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        enemyRb.velocity = Vector2.zero;
        enemyRb.angularVelocity = 0f;

        enemyRb.bodyType = RigidbodyType2D.Kinematic;
    }

    // 공격 속도 오로라 스킬 적용 코루틴
    public IEnumerator ApplyAttackSpeedAurora()
    {
        skillCoroutineArr[0] = ApplyAttackSpeedAurora();

        while (PlayerManager.instance.stats.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(9f);

            attakSpeedAuroraSprite.gameObject.SetActive(true);
            IncreasedAttackSpeed(0.625f);

            yield return new WaitForSeconds(2f);

            attakSpeedAuroraSprite.gameObject.SetActive(false);
            IncreasedAttackSpeed(-0.625f);
        }
    }

    // 크리티컬 오로라 스킬 적용 코루틴
    public IEnumerator ApplyCriticalAurora()
    {
        skillCoroutineArr[1] = ApplyCriticalAurora();

        while (PlayerManager.instance.stats.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(9f);

            criticalAuroraSprite.gameObject.SetActive(true);
            criticalChance += 47;

            yield return new WaitForSeconds(2f);

            criticalAuroraSprite.gameObject.SetActive(false);
            criticalChance -= 47;
        }
    }

    // 스킬 코루틴 중단(사망 시 호출)
    public void StopBowSkillCoroutine()
    {
        foreach (var skillCoroutine in skillCoroutineArr)
        {
            if (skillCoroutine != null)
                StopCoroutine(skillCoroutine);
        }
    }

    // 첫 타겟을 찾을 때 생명주기 때문에 타겟을 찾지 못하는 버그 해결용
    private IEnumerator FindFirstTarget()
    {
        yield return new WaitForSeconds(0.5f);

        UpdateEnemyList();
        target = FindTarget();
    }
}