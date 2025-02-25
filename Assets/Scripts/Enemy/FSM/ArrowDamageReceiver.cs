using UnityEngine;
using System.Collections;

public class ArrowDamageReceiver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Arrow_Bow arrow = collision.GetComponent<Arrow_Bow>();
        if (arrow != null)
        {
            // ������ ���� 0�̸� �����̸� �ְ� �ٽ� Ȯ��
            if (arrow.DamageValue == 0)
            {
                StartCoroutine(DelayedDamage(arrow));
            }
            else
            {
                ApplyDamage(arrow);
            }
        }
    }

    private IEnumerator DelayedDamage(Arrow_Bow arrow)
    {
        yield return null; // �� ������ ��ٸ�

        if (arrow != null)
        {
            ApplyDamage(arrow);
        }
    }

    private void ApplyDamage(Arrow_Bow arrow)
    {
        EnemyAI enemy = GetComponentInParent<EnemyAI>();
        if (enemy != null)
        {
            if (!enemy.IsDead)
            {
                int damage = arrow.DamageValue;
                Debug.Log($"�������� �޾ƿ� (������): {damage}");
                enemy.TakeDamage(damage);

                ArrowManager arrowManager = arrow.GetComponentInParent<ArrowManager>();
                if (arrowManager != null)
                {
                    arrowManager.ReturnArrow(arrow.gameObject);
                }
                else
                {
                    Destroy(arrow.gameObject);
                }
            }
        }
    }
}
