using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour    //�÷��̾�� �浹�Ұ�� �������� ��
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            // �÷��̾ �������°� �ƴϰ� ü���� 0���� ũ�� ������ ����
            if (!playerStats.IsInvincivility && playerStats.CurrentHealth > 0)
            {
                playerStats.OnDamaged(10);
                Debug.Log(playerStats.CurrentHealth);
            }
        }
    }
}
