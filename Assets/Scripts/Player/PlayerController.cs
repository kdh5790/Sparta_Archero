using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer playerSprite;

    private Vector2 moveVelocity;
    private Vector2 moveDirection;

    private bool isMove;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();

        if(!isMove)
        {
            Debug.Log("���� ����");
        }
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // �̵� ������ �ƴ��� üũ �Ͽ� �ִϸ��̼� ����
        if (animator != null)
        {
            animator.SetBool("IsMove", moveInput.magnitude == 0 ? false : true);
            isMove = animator.GetBool("IsMove");
        }

        moveVelocity = moveInput.normalized * moveSpeed; // �̵� �ӵ� ���ϱ�
        rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime); // �̵�
        moveDirection = moveVelocity.normalized; // �̵����� ���� ���ϱ�

        FlipSprite(moveDirection.x); // �̵� ���⿡ ���� ��������Ʈ ����
    }

    void FlipSprite(float directionX)
    {
        playerSprite.flipX = directionX < 0 ? true : false;
    }
}
