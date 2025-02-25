using UnityEngine;
using Utils;  // TargetingUtils가 있는 네임스페이스

public abstract class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 3f;
    public Transform target; // 타겟, 기본적으로 플레이어로 할당

    protected bool isDead = false;

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriter;

    // Awake에서 컴포넌트 초기화 및 타겟 감지
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponentInChildren<SpriteRenderer>();

        // target이 할당되어 있지 않으면 "Player" 태그를 가진 오브젝트를 찾아 할당
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    // 물리 업데이트에서 이동 처리
    protected virtual void FixedUpdate()
    {
        if (isDead || target == null) return;
        MoveTowardsTarget();
    }

    // LateUpdate에서 스프라이트 좌우 반전 처리
    protected virtual void LateUpdate()
    {
        if (isDead || target == null) return;
        spriter.flipX = target.position.x < transform.position.x;
    }

    // 타겟 방향으로 이동
    public virtual void MoveTowardsTarget()
    {
        Vector3 direction = TargetingUtils.GetDirection(transform, target);
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + (Vector2)movement);
    }
}
