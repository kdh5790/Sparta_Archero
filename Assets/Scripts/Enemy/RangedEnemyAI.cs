using UnityEngine;
using System.Collections;

public class RangedEnemyAI : EnemyAI
{
    public GameObject projectilePrefab;  // �߻�ü ������
    public float projectileSpeed = 10f;
    public float attackDelay = 0.5f;

    protected override void Attack()
    {
        // ���Ÿ� ���� ����: ���� �ð� �� ����ü �߻�
        Debug.Log(name + "���Ÿ� �߻�test");
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
