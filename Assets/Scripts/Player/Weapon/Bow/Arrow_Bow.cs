using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public GameObject target;

    private int damage;
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

    private void MoveToTarget()
    {
        rigidBody.velocity = transform.up * 4.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.enabled) return;

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

            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
        }
    }


}
