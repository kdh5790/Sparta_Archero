using System.Collections;
using UnityEngine;

public class BossEnemyAI : MonoBehaviour
{
    private BasicEnemyAI enemyAI;
    private Transform player;

    [Header("����� ����")]
    public float shockwaveCooldown = 5f; // ����� ��Ÿ��
    private float lastShockwaveTime = 0f;
    public float shockwaveRadius = 3f; // ����� ����
    public int shockwaveDamage = 50; // ����� ������
    

    public Animator shockwaveAnimator;

    [Header("�г� ����")]
    private bool isEnraged = false;

    [Header("���� ����")]
    private bool isInvincible = false;
    public float invincibleDuration = 3f; // ���� ���� �ð�

    void Start()
    {
        enemyAI = GetComponent<BasicEnemyAI>();


       if (shockwaveAnimator == null)
        {
            Transform shockwaveTransform = transform.Find("Shockwave");
            if (shockwaveTransform != null)
            {
                shockwaveAnimator = shockwaveTransform.GetComponent<Animator>();
            }
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (enemyAI == null || player == null || enemyAI.IsDead) return;

        // ����� ���� �ߵ�
        if (Time.time - lastShockwaveTime >= shockwaveCooldown)
        {
            lastShockwaveTime = Time.time;
            StartCoroutine(CastShockwave());
        }

        // ü���� 30% ������ �� �г� ���
        if (!isEnraged && enemyAI.CurrentHealth <= enemyAI.MaxHealth * 0.3f)
        {
            isEnraged = true;
            enemyAI.instanceSpeed += 1f;
            Debug.Log("������ �г� ����. �ӵ� ����");
        }
    }

    // ����� ���� ����
    IEnumerator CastShockwave()
    {
        Debug.Log("������ ����ĸ� ���");
        shockwaveAnimator.SetTrigger("Shockwave"); // �ִϸ��̼� Ʈ����



        yield return new WaitForSeconds(0.5f); // ����� �ִϸ��̼��� ���� �ð�

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius);
        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.OnDamaged(shockwaveDamage);
                }
            }
        }
    }

    // ���� ���� (ü�� 20% ������ �� �ߵ�)
    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("������ ���� ���¶� ���ظ� ���� ����");
            return;
        }

        enemyAI.TakeDamage(damage);

        if (enemyAI.CurrentHealth <= enemyAI.MaxHealth * 0.2f && !isInvincible)
        {
            StartCoroutine(ActivateInvincibility());
        }
    }

    IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        Debug.Log("���� ���� ����");

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        Debug.Log("���� ���� ���� ����");
    }
}
