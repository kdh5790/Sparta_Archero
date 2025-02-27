using UnityEngine;

public class Projectile : MonoBehaviour     //���� ����ü
{
    public int damage = 50; // �⺻ ������

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
        }
    }

    void OnTriggerEnter2D(Collider2D collision)     //�÷��̾�� �浹��
    {

        if (collision.CompareTag("Player"))     //�±�Ȯ��
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                Debug.Log($" �÷��̾�� {damage} ������"); //������ �ֱ�
                playerStats.OnDamaged(damage);
            }

            Destroy(gameObject);        //����ü �ı�
        }
    }
}
