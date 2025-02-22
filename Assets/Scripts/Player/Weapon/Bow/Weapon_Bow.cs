using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Weapon_Bow : MonoBehaviour
{
    private int damage = 30;
    public int Damage { get { return damage; } set { damage = value; } }

    private float attackSpeed = 1f;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    private float criticalDamage = 1.5f;
    public float CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }

    private int criticalChance = 0;
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    private bool isPiercingShot = false;
    public bool IsPiercingShot { get { return isPiercingShot; } set { isPiercingShot = value; } }

    private bool isRebound = false;
    public bool IsRebound { get { return isRebound; } set { isRebound = value; } }


    public List<GameObject> enemyList = new List<GameObject>(); // 필드의 적들을 담을 리스트
    public GameObject target; // 공격해야 할 타겟

    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();

        playerController = GetComponentInParent<PlayerController>();

        UpdateEnemyList();
        target = FindTarget();
    }

    void Update()
    {
        if (playerController.isMove || target == null)
            target = FindTarget();

        LookAtTarget();

        // 플레이어가 멈춰있음 + 타겟이 있음 + 멈추고 일정시간이 지남
        if (!playerController.isMove && target != null && playerController.stopTime > 0.4f)
            animator.SetBool("IsAttack", true);
        else
            animator.SetBool("IsAttack", false);
    }

    // 현재 활성화 된 적들 찾아오기                 태그가 "Enemy"이고 활성화 된 적들을 찾아 리스트로 변환
    public void UpdateEnemyList() => enemyList = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemy") && x.activeSelf).ToList();


    // 가장 가까운 적 찾기
    public GameObject FindTarget(Transform _transform = null, GameObject _enemy = null)
    {
        if (target != null && !target.activeSelf || target == null)
            UpdateEnemyList();


        GameObject go = null;

        if (enemyList != null)
        {
            float targetDistance = 100f;

            foreach (GameObject obj in enemyList)
            {
                // 화살의 다음타겟을 찾을 경우 현재 자신(화살)의 타겟은 넘어가기
                if (_enemy != null && obj.name == _enemy.name)
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
        }
        else
        {
            return null;
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
            PlayerManager.instance.arrowManager.StartShootDelegate(target);
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
}
