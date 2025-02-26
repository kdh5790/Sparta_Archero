using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    HPPotion
}

public class DropItem : MonoBehaviour
{
    [SerializeField] private Item item;

    private PlayerStats player;
    private Rigidbody2D rigid;

    private void OnEnable()
    {
        if (PlayerManager.instance.stats != null)
            player = PlayerManager.instance.stats;

        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(Random.Range(-200, 200), Random.Range(-200, 200)), ForceMode2D.Force);
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.2f);

        rigid.velocity = Vector3.zero;
        rigid.bodyType = RigidbodyType2D.Kinematic;


        while (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 1)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rigid.velocity = new Vector2(direction.x, direction.y) * 8f;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (item)
            {
                case Item.HPPotion:
                    player.CurrentHealth += 200;
                    player.CurrentHealth = Mathf.Min(player.CurrentHealth, player.MaxHealth);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
