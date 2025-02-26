using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyAIHpUI : MonoBehaviour
{
    GameObject enemy_Ai;
    BasicEnemyAI enemy = null;
    Vector3 temp = new Vector3();

    public RectTransform hpFront;

    private void Awake()
    {
        enemy_Ai = transform.parent.gameObject;
        enemy = transform.GetComponentInParent<BasicEnemyAI>();
        hpFront = transform.Find("Front").GetComponent<RectTransform>();
        temp = hpFront.localScale;
    }

    private void FixedUpdate()
    {
        hpFront.localScale = new Vector3(enemy.CurrentHealth / enemy.MaxHealth, temp.y, temp.z);
    }

}
