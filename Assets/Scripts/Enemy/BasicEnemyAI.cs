using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : EnemyAI
{
    [Header("Enemy Stat Asset")]
    public EnemyStat enemyStat;

    [Header("Instance Stats (Modifiable in Runtime)")]
    [SerializeField] public int instanceHealth;
    [SerializeField] public int instanceAttack;
    [SerializeField] public int instanceDefense;
    [SerializeField] public float instanceSpeed;
    [SerializeField] public int instanceExp;

    // �ν��Ͻ� �ʱ�ȭ ���� (�� ���� �⺻�� ����)
    private bool initialized = false;

    protected override void Awake()
    {
        base.Awake();
        if (enemyStat != null && !initialized)
        {
            ApplyDefaults();
            initialized = true;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // �׽�Ʈ��
        if (!Application.isPlaying)
        {
            if (enemyStat != null)
            {
                ApplyDefaults();
            }
        }
    }
#endif

    // ScriptableObject�� �⺻���� �ν��Ͻ� ������ �����ϰ�, �θ� ü�µ� ����
    void ApplyDefaults()
    {
        instanceHealth = enemyStat.health;
        instanceAttack = enemyStat.attack;
        instanceDefense = enemyStat.defense;
        instanceSpeed = enemyStat.speed;
        instanceExp = enemyStat.exp;

        maxHealth = enemyStat.health;
        currentHealth = enemyStat.health;
    }

    // �θ��� MoveTowardsTarget ��� instanceSpeed�� ����Ͽ� �̵�
    public override void MoveTowardsTarget()
    {
        Vector3 direction = Utils.TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * instanceSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }
}
