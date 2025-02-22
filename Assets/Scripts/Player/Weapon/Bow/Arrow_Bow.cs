using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public GameObject target;

    private int damage;
    private int bound = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bow = FindObjectOfType<Weapon_Bow>();
    }

    private void Update()
    {
        MoveToTarget();

        if (Vector3.Distance(transform.parent.position, transform.position) > 12f)
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
    }

    private void OnEnable()
    {
        bound = 0;
        if (bow != null) target = bow.target;
    }

    private void MoveToTarget()
    {
        rigidBody.velocity = transform.up * 4.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            bool isCritical = bow.CalculateCriticalChance();

            if (isCritical)
            {
                damage = (int)(Random.Range(bow.Damage * (bow.CriticalDamage - 0.1f), bow.Damage * (bow.CriticalDamage + 0.1f)));
                Debug.Log($"利 面倒 | 农府萍拿 单固瘤 : {damage}");
            }

            else
            {
                damage = (int)(Random.Range(bow.Damage * 0.9f, bow.Damage * 1.1f));
                Debug.Log($"利 面倒 | 单固瘤 : {damage}");
            }

            if (bow.IsRebound && bound < 2)
            {
                target = bow.FindTarget(transform, target);
                if (target != null)
                {
                    float angle = PlayerManager.instance.arrowManager.LookAtTargetForArrow(target, transform);
                    transform.rotation = Quaternion.identity;
                    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                    bound++;

                    return;
                }
            }

            if (!bow.IsPiercingShot)
            {
                GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
                return;
            }
        }
    }
}
