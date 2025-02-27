using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionA : MonoBehaviour
{
    public int damage = 20; // 장애물이 주는 데미지
    public float damageInterval = 1.0f; // 데미지를 주는 간격 (초)
    private bool isDamaging = false; // 데미지 코루틴 실행 여부

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDamaging)
        {
            StartCoroutine(DealDamageOverTime(collision.GetComponent<PlayerStats>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamaging = false; // 충돌에서 벗어나면 데미지 중지
        }
    }

    private IEnumerator DealDamageOverTime(PlayerStats player)
    {
        isDamaging = true;

        while (isDamaging && player != null)
        {
            player.OnDamaged(damage);
            yield return new WaitForSeconds(damageInterval);
        }

    }
}