using UnityEngine;

// 플레이어 추격 상태
public class ChaseState : EnemyState
{
    public ChaseState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("추격 상태 진입");
    }

    public override void Execute()
    {
        // 플레이어 방향으로 이동
       // enemy.MoveTowardsPlayer();
    }

    public override void Exit()
    {
        Debug.Log("추격 상태 종료");
    }
}
