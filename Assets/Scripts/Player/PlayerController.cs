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

    private float moveSpeed;
    public bool isMove;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();

        moveSpeed = FindObjectOfType<PlayerStats>().Speed;
    }

    void FixedUpdate()
    {
        Move();

        if (!isMove)
        {
            Debug.Log("공격 상태");
        }
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
