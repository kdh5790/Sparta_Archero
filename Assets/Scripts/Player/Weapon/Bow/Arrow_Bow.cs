using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public BasicEnemyAI target;
    public int DamageValue { get { return damage; } }   //enemy쪽에서 데미지 값 참조하려고 넣었습니다

    private int damage; // 데미지
    private int bound = 0; // 반동 횟수
    private bool isPiercing = false; // 관통 스킬 보유 시 관통 확인용

    private const float arrowSpeed = 12f; // 화살 속도
    private const float maxDistance = 15f; // 플레이어와 화살의 최대 거리(최대 거리를 넘어가면 화살 비활성화)

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bow = FindObjectOfType<Weapon_Bow>();
    }

    private void Update()
    {
        MoveToTarget();

        // 플레이어와 일정 거리 이상 멀어졌다면 화살 회수(일정거리는 맵 크기보다 크게 설정)
        if (Vector3.Distance(transform.parent.position, transform.position) > maxDistance)
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
    }

    private void OnEnable()
    {
        bound = 0;
        isPiercing = false;

        if (bow != null) target = bow.target;
    }

    // 타겟 방향으로 이동
    private void MoveToTarget()
    {
        rigidBody.velocity = transform.up * arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 크리티컬 여부 확인
            bool isCritical = bow.CalculateCriticalChance();

            // 크리티컬 or 일반 데미지 주기
            if (isCritical)
                damage = (int)(bow.Damage * bow.CriticalDamage);

            else
                damage = bow.Damage;

            if (bow.IsRage)
                CalculateRageDamage();

            // 반동 횟수에 따라 데미지 감소(최대 2)
            if (bound > 0)
            {
                for (int i = 0; i < bound; i++)
                {
                    damage = (int)(damage * 0.7f);
                }
            }

            // 이전에 적을 관통 했다면 데미지 감소
            if (isPiercing)
                damage = (int)(damage * 0.67f);

            // 멀티샷 스킬을 보유중이라면 최종 데미지 10% 감소
            if (bow.IsMultiShot)
                damage = (int)(damage * 0.9f);

            Debug.Log(isCritical ? $"적 충돌 | 크리티컬 데미지 : {damage}" : $"적 충돌 | 데미지 : {damage}");

            BasicEnemyAI enemy = collision.GetComponent<BasicEnemyAI>();

            if (enemy != null)
                enemy.TakeDamage(damage);

            // 반동 스킬 보유 + 현재 튕긴 횟수가 2보다 작다면 다음 타겟 찾아 이동시킴
            if (bow.IsRebound && bound < 2)
            {
                target = bow.FindTarget(transform, collision.gameObject);
                if (target != null)
                {
                    bow.KnockBackEnemy(collision.transform, transform.position);

                    float angle = PlayerManager.instance.arrowManager.LookAtTargetForArrow(target.gameObject, transform);
                    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                    bound++;

                    return;
                }
            }

            // 관통샷 스킬을 보유하지 않았다면 데미지를 입힌 후 화살 비활성화
            if (!bow.IsPiercingShot)
            {
                bow.KnockBackEnemy(collision.transform, transform.position);
                GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
                return;
            }

            isPiercing = true;
        }
    }

    // 분노 스킬 보유 시 데미지 계산
    private void CalculateRageDamage()
    {
        float currentHP = PlayerManager.instance.stats.CurrentHealth;
        float maxHP = PlayerManager.instance.stats.MaxHealth;

        float percentage = (maxHP - currentHP) / maxHP * 100f;
        percentage = 1f + (percentage * 0.012f);

        damage = (int)(damage * percentage);
    }


}