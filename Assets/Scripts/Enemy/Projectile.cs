using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 50; // �⺻ ������

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
                Debug.Log($" �÷��̾�� {damage} ������");
                playerStats.OnDamaged(damage);
            }

            Destroy(gameObject);
        }
    }
}
