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

    // 인스턴스 초기화 여부 (한 번만 기본값 적용)
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
        // 테스트용
        if (!Application.isPlaying)
        {
            if (enemyStat != null)
            {
                ApplyDefaults();
            }
        }
    }
#endif

    // ScriptableObject의 기본값을 인스턴스 변수에 적용하고, 부모 체력도 설정
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

    // 부모의 MoveTowardsTarget 대신 instanceSpeed를 사용하여 이동
    public override void MoveTowardsTarget()
    {
        Vector3 direction = Utils.TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * instanceSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }
}
