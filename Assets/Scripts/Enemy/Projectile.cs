using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 50; // 기본 데미지

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

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
