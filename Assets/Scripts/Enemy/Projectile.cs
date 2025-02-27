using UnityEngine;

public class Projectile : MonoBehaviour     //몬스터 투사체
{
    public int damage = 50; // 기본 데미지

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
        }
    }

    void OnTriggerEnter2D(Collider2D collision)     //플레이어와 충돌시
    {

        if (collision.CompareTag("Player"))     //태그확인
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                Debug.Log($" 플레이어에게 {damage} 데미지"); //데미지 주기
                playerStats.OnDamaged(damage);
            }

            Destroy(gameObject);        //투사체 파괴
        }
    }
}
