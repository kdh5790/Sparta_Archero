using UnityEngine;

// �÷��̾� �߰� ����
public class ChaseState : EnemyState
{
    public ChaseState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("�߰� ���� ����");
    }

    public override void Execute()
    {
        // �÷��̾� �������� �̵�
       // enemy.MoveTowardsPlayer();
    }

    public override void Exit()
    {
        Debug.Log("�߰� ���� ����");
    }
}
