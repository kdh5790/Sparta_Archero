using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionA : MonoBehaviour
{
    public int damage = 20; // ��ֹ��� �ִ� ������
    public float damageInterval = 1.0f; // �������� �ִ� ���� (��)
    private bool isDamaging = false; // ������ �ڷ�ƾ ���� ����

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
            isDamaging = false; // �浹���� ����� ������ ����
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