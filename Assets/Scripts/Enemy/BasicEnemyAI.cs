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


    void ApplyDefaults()
    {
        instanceHealth = enemyStat.health;
        instanceAttack = enemyStat.attack;
        instanceDefense = enemyStat.defense;
        instanceSpeed = enemyStat.speed;
        instanceExp = enemyStat.exp;  // 경험치 값 추가

        maxHealth = enemyStat.health;
        currentHealth = enemyStat.health;
        exp = enemyStat.exp;  // 경험치 적용
    }

   

    public override void MoveTowardsTarget()
    {
        Vector3 direction = Utils.TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * instanceSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }
}