using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 근접 공격 상태
public class MeleeAttackState : EnemyState
{
    public MeleeAttackState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("근접 공격 상태 진입");
    }

    public override void Execute()
    {
        // 근접 공격 실행 (EnemyAI에 구현된 Attack() 호출)
    //    enemy.Attack();
    }

    public override void Exit()
    {
        Debug.Log("근접 공격 상태 종료");
    }
}