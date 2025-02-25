using UnityEngine;

// 상태 기본 클래스
public abstract class EnemyState
{
    protected EnemyAI enemy;

    public EnemyState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    // 상태 진입 시 실행
    public abstract void Enter();
    // 상태 실행(매 프레임 호출)
    public abstract void Execute();
    // 상태 종료 시 실행
    public abstract void Exit();
}