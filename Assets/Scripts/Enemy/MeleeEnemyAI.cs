using UnityEngine;

public class MeleeEnemyAI : EnemyAI
{
    protected override void Attack()
    {
        // 근접 공격 로직 구현


        Debug.Log(name + " : 근접 공격test");
    }
}
