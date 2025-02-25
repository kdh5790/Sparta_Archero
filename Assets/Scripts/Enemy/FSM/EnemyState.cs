using UnityEngine;

// ���� �⺻ Ŭ����
public abstract class EnemyState
{
    protected EnemyAI enemy;

    public EnemyState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    // ���� ���� �� ����
    public abstract void Enter();
    // ���� ����(�� ������ ȣ��)
    public abstract void Execute();
    // ���� ���� �� ����
    public abstract void Exit();
}