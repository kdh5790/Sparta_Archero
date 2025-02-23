using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public GameObject target;

    private int damage; // 데미지
    private int bound = 0; // 반동 횟수

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

        if (Vector3.Distance(transform.parent.position, transform.position) > maxDistance)
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
    }

    private void OnEnable()
    {
        bound = 0;
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
            {
                damage = (int)(Random.Range(bow.Damage * (bow.CriticalDamage - 0.1f), bow.Damage * (bow.CriticalDamage + 0.1f)));
                Debug.Log($"적 충돌 | 크리티컬 데미지 : {damage}");
            }

            else
            {
                damage = (int)(Random.Range(bow.Damage * 0.9f, bow.Damage * 1.1f));
                Debug.Log($"적 충돌 | 데미지 : {damage}");
            }

            // 반동 스킬 보유 + 현재 튕긴 횟수가 2보다 작다면 다음 타겟 찾아 이동시킴
            if (bow.IsRebound && bound < 2)
            {
                target = bow.FindTarget(transform, collision.gameObject);
                if (target != null)
                {
                    float angle = PlayerManager.instance.arrowManager.LookAtTargetForArrow(target, transform);
                    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                    bound++;

                    return;
                }
            }

            // 관통샷 스킬을 보유하지 않았다면 데미지를 입힌 후 화살 비활성화
            if (!bow.IsPiercingShot)
            {
                GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
                return;
            }
        }
    }
}
