using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����
public class MeleeAttackState : EnemyState
{
    public MeleeAttackState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("���� ���� ���� ����");
    }

    public override void Execute()
    {
        // ���� ���� ���� (EnemyAI�� ������ Attack() ȣ��)
    //    enemy.Attack();
    }

    public override void Exit()
    {
        Debug.Log("���� ���� ���� ����");
    }
}