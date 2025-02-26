using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 50; // �⺻ ������

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError(" Projectile�� Rigidbody2D�� ����! Rigidbody2D�� �߰��ϼ���.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($" ����ü �浹 ����: {collision.gameObject.name}");

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
