using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_Bow : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>(); // 필드의 적들을 담을 리스트
    public GameObject target; // 공격해야 할 타겟

    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();

        playerController = GetComponentInParent<PlayerController>();

        UpdateEnemyList();
    }

    void Update()
    {
        if (playerController.isMove)
            FindTarget();

        if (target != null)
            LookAtTarget();

        // 플레이어가 멈춰있음 + 타겟이 있음 + 멈추고 일정시간이 지남
        if (!playerController.isMove && target != null && playerController.stopTime > 0.4f)
            animator.SetBool("IsAttack", true);
        else
            animator.SetBool("IsAttack", false);
    }

    // 현재 활성화 된 적들 찾아오기                 태그가 "Enemy"인 적들을 찾아 리스트로 변환
    public void UpdateEnemyList() => enemyList = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemy")).ToList();


    // 가장 가까운 적 찾기
    private void FindTarget()
    {
        float targetDistance = 100f;

        foreach (GameObject obj in enemyList)
        {
            // 적과 플레이어의 거리 구하기
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            // 가장 가까운 적 찾기
            if (distance < targetDistance)
            {
                targetDistance = distance;
                target = obj;
            }
        }

    }

    // 타겟 바라보기
    private void LookAtTarget()
    {
        // 바라보는 방향 구하기
        Vector2 direction = target.transform.position - transform.position;

        // 방향 각도 계산 후 라디안 값을 도 단위로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z 회전값으로 타겟 바라보기
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
