using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 50; // 기본 데미지

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError(" Projectile에 Rigidbody2D가 없음! Rigidbody2D를 추가하세요.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($" 투사체 충돌 감지: {collision.gameObject.name}");

        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                Debug.Log($" 플레이어에게 {damage} 데미지");
                playerStats.OnDamaged(damage);
            }

            Destroy(gameObject);
        }
    }
}
