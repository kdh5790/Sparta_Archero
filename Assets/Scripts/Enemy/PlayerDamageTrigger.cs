using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour    //플레이어와 충돌할경우 데미지를 줌
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            // 플레이어가 무적상태가 아니고 체력이 0보다 크면 데미지 적용
            if (!playerStats.IsInvincivility && playerStats.CurrentHealth > 0)
            {
                playerStats.OnDamaged(10);
                Debug.Log(playerStats.CurrentHealth);
            }
        }
    }
}
