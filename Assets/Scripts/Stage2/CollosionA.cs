using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �÷��̾ ��ֹ��� �浹������ �������� �ִ� ��ũ��Ʈ(Stage_11 ~ Stage_19)
public class CollosionA : MonoBehaviour
{
    public int damage = 20; //��ֹ��� �ִ� ������
    public float damageInterval = 1.0f; //�������� �ִ� ���� (��)
    private bool isDamaging = false; //������ �ڷ�ƾ ���� ����

    private void OnTriggerEnter2D(Collider2D collision) //�÷��̾ ��ֹ��� �浹������ ȣ��Ǵ� �Լ�
    {
        if (collision.CompareTag("Player") && !isDamaging) //�÷��̾�� �浹�ϰ� ������ �ڷ�ƾ�� ���������� ������
        {
            StartCoroutine(DealDamageOverTime(collision.GetComponent<PlayerStats>())); // ������ �ڷ�ƾ ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //�÷��̾ ��ֹ����� ������� ȣ��Ǵ� �Լ�
    {
        if (collision.CompareTag("Player"))
        {
            isDamaging = false; //�浹���� ����� ������ ����
        }
    }

    private IEnumerator DealDamageOverTime(PlayerStats player) //�������� �ִ� �ڷ�ƾ
    {
        isDamaging = true; //������ �ڷ�ƾ ������

        while (isDamaging && player != null) //������ �ڷ�ƾ�� �������̰� �÷��̾ �����Ҷ�
        {
            player.OnDamaged(damage); //�÷��̾�� �������� ��
            yield return new WaitForSeconds(damageInterval); //������ ���ݸ�ŭ ���
        }

    }
}