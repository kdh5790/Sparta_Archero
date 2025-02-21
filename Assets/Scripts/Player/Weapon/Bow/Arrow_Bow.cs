using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bow : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Weapon_Bow bow;
    public GameObject target;

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

        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Àû Ãæµ¹");
            GetComponentInParent<ArrowManager>().ReturnArrow(gameObject);
        }
    }


}
