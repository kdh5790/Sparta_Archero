using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyPatternManager�� ���� ��ȯ ����
public class EnemyPatternManager : MonoBehaviour
{
    private EnemyState currentState;
    private EnemyAI enemy;

    // �� ���� �ν��Ͻ� ����
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
        // �ʱ� ���¸� �߰� ���·� ����
        ChangeState(chaseState);
    }

    void Update()
    {
        // ���� ���� ����
        if (currentState != null)
        {
            currentState.Execute();
        }
        // ���÷�, �÷��̾���� �Ÿ��� ���� ���� ��ȯ ��
     //   EvaluateState();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    // ����: �÷��̾���� �Ÿ��� �������� ���� ��ȯ
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
