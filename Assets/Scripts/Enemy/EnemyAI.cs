using UnityEngine;
using Utils;  // TargetingUtils�� �ִ� ���ӽ����̽�

public abstract class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 3f;
    public Transform target; // Ÿ��, �⺻������ �÷��̾�� �Ҵ�

    protected bool isDead = false;

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriter;

    // Awake���� ������Ʈ �ʱ�ȭ �� Ÿ�� ����
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponentInChildren<SpriteRenderer>();

        // target�� �Ҵ�Ǿ� ���� ������ "Player" �±׸� ���� ������Ʈ�� ã�� �Ҵ�
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    // ���� ������Ʈ���� �̵� ó��
    protected virtual void FixedUpdate()
    {
        if (isDead || target == null) return;
        MoveTowardsTarget();
    }

    // LateUpdate���� ��������Ʈ �¿� ���� ó��
    protected virtual void LateUpdate()
    {
        if (isDead || target == null) return;
        spriter.flipX = target.position.x < transform.position.x;
    }

    // Ÿ�� �������� �̵�
    public virtual void MoveTowardsTarget()
    {
        Vector3 direction = TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }
}
