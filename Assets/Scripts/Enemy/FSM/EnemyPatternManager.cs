using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyPatternManager로 상태 전환 관리
public class EnemyPatternManager : MonoBehaviour
{
    private EnemyState currentState;
    private EnemyAI enemy;

    // 각 상태 인스턴스 생성
    private ChaseState chaseState;
    private MeleeAttackState meleeState;

    void Awake()
    {
        enemy = GetComponent<EnemyAI>();
        chaseState = new ChaseState(enemy);
        meleeState = new MeleeAttackState(enemy);
    }

    void Start()
    {
        // 초기 상태를 추격 상태로 설정
        ChangeState(chaseState);
    }

    void Update()
    {
        // 현재 상태 실행
        if (currentState != null)
        {
            currentState.Execute();
        }
        // 예시로, 플레이어와의 거리에 따라 상태 전환 평가
     //   EvaluateState();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    // 예시: 플레이어와의 거리를 기준으로 상태 전환
    /*
    public void EvaluateState()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distance > enemy.attackDistance)
        {
            if (!(currentState is ChaseState))
                ChangeState(chaseState);
        }
        else
        {
            if (!(currentState is MeleeAttackState))
                ChangeState(meleeState);
        }
    }
    */
}
