using UnityEngine;
using System.Collections;

public class RangedEnemyAI : EnemyAI
{
    public GameObject projectilePrefab;  // 발사체 프리팹
    public float projectileSpeed = 10f;
    public float attackDelay = 0.5f;

    protected override void Attack()
    {
        // 원거리 공격 로직: 일정 시간 후 투사체 발사
        Debug.Log(name + "원거리 발사test");
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        if (projectilePrefab != null && player != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 direction = (player.position - transform.position).normalized;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = direction * projectileSpeed;
        }
    }
}
