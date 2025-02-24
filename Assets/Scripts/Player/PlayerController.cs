using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer playerSprite;

    private Vector2 moveVelocity;
    private Vector2 moveDirection;

    private float moveSpeed = 3f;

    public float stopTime; // 플레이어가 멈춰있던 시간(공격 체크 용)
    public bool isMove;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();

        if (!isMove)
            stopTime += Time.deltaTime;

        else
            stopTime = 0;

        UIManager.Instance.UpdatePlayerUIPosition(rigidBody.position); //플레이어 HP Bar UI 추적용
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 이동 중인지 아닌지 체크 하여 애니메이션 변경
        if (animator != null)
        {
            animator.SetBool("IsMove", moveInput.magnitude == 0 ? false : true);
            isMove = animator.GetBool("IsMove");
        }

        moveVelocity = moveInput.normalized * moveSpeed; // 이동 속도 구하기
        rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime); // 이동
        moveDirection = moveVelocity.normalized; // 이동중인 방향 구하기

        FlipSprite(moveDirection.x); // 이동 방향에 따라 스프라이트 조절
    }

    void FlipSprite(float directionX)
    {
        if (directionX != 0)
            playerSprite.flipX = directionX < 0;
    }
}