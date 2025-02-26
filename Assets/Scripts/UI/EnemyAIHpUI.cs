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
        float cup = enemy.CurrentHealth / enemy.MaxHealth * temp.x;
        if(cup < 0)
        {
            cup = 0;
        }
        hpFront.localScale = new Vector3(cup, temp.y, temp.z);
    }

}
