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

    public float stopTime; // �÷��̾ �����ִ� �ð�(���� üũ ��)
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

        UIManager.Instance.UpdatePlayerUIPosition(rigidBody.position); //�÷��̾� HP Bar UI ������
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
        if (directionX != 0)
            playerSprite.flipX = directionX < 0;
    }
}