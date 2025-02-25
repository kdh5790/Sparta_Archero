using UnityEngine;
using System.Collections;

public class ArrowDamageReceiver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Arrow_Bow arrow = collision.GetComponent<Arrow_Bow>();
        if (arrow != null)
        {
            // 데미지 값이 0이면 딜레이를 주고 다시 확인
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
        yield return null; // 한 프레임 기다림

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
                Debug.Log($"데미지값 받아옴 (딜레이): {damage}");
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
