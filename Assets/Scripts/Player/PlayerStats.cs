using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.U2D;

public class PlayerStats : MonoBehaviour
{
    private int level;
    public int Level { get { return level; } set { level = value; } }

    private float currentExp;
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }

    private float maxExp;
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }

    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    private int maxHealth;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    private int dodgeChance;
    public int DodgeChance { get { return dodgeChance; } set { dodgeChance = value; } }

    private bool isInvincivility;
    public bool IsInvincivility { get { return isInvincivility; } set { isInvincivility = value; } }

    private SpriteRenderer sprite;
    private IEnumerator invincibilityCoroutine;

    public void InitPlayerStats()
    {
        level = 1; currentExp = 0; maxExp = 100; currentHealth = 600; maxHealth = 600; speed = 3f;
        sprite = GetComponentInChildren<SpriteRenderer>();

        UIManager.Instance.UpdatePlayerHP(maxHealth, currentHealth); //�÷��̾� ui ������Ʈ��
        UIManager.Instance.UpdatePlayerExp(maxExp, currentExp); //�÷��̾� ui ������Ʈ��
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnDamaged(300);
        }

        if (Input.GetKeyDown(KeyCode.P)) //ui �׽�Ʈ���Դϴ�
        {
            IncreaseExp(30);
        }

    }

    public void OnDamaged(int damage)
    {
        if (IsInvincivility)
        {
            Debug.Log($"���� �����̹Ƿ� {damage}�� �������� ���� �ʽ��ϴ�.");
            return;
        }

        if (Random.Range(0, 100) < DodgeChance)
        {
            Debug.Log("ȸ�ǿ� �����߽��ϴ�.");
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        UIManager.Instance.UpdatePlayerHP(maxHealth, currentHealth); //�÷��̾� ui ������Ʈ��

        if (currentHealth <= 0)
        {
            PlayerDead();
            return;
        }

        StartCoroutine(ApplyInvincibilityAfterDamage());
    }

    public void IncreaseExp(int exp)
    {
        currentExp += exp;

        UIManager.Instance.UpdatePlayerExp(maxExp, currentExp); //�÷��̾� ui ������Ʈ��

        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= maxExp;
        maxExp *= 1.2f;

        level++;

        UIManager.Instance.UpdatePlayerExp(maxExp, currentExp); //�÷��̾� ui ������Ʈ��
        UIManager.Instance.LevelUpUI();// ��ų ȹ�� UI ON
        Debug.Log($"������! Lv.{level}, MaxExp:{maxExp}");
    }

    public void PlayerDead()
    {
        Debug.Log("�÷��̾� ���");
        PlayerManager.instance.isDead = true;
        StartCoroutine(PlayerSpriteColorChange());
        GetComponent<PlayerController>().animator.speed = 0;

        if (invincibilityCoroutine != null)
            StopCoroutine(invincibilityCoroutine);

        PlayerManager.instance.bow.StopBowSkillCoroutine();
    }

    // �������� ���� �� ��������
    public IEnumerator ApplyInvincibilityAfterDamage()
    {
        invincibilityCoroutine = ApplyInvincibilityAfterDamage();

        IsInvincivility = true;

        int count = 3; // ������ Ƚ��

        for (int i = 0; i < count; i++)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.2f);
            yield return new WaitForSeconds(0.1f);

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            yield return new WaitForSeconds(0.1f);
        }

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        IsInvincivility = false;
    }

    // ���� ��ų ���� �� 10�� ���� 2�ʰ� ����
    public IEnumerator ApplyInvincibilitySkill()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(10f);

            Debug.Log("���� ����");
            IsInvincivility = true;
            sprite.color = new Color(0.25f, 0.35f, 1);

            yield return new WaitForSeconds(2f);

            Debug.Log("���� ����");
            IsInvincivility = false;
            sprite.color = Color.white;
        }
    }

    public IEnumerator PlayerSpriteColorChange()
    {
        Debug.Log("asdsa");

        float duration = 1f; // ���� ���濡 �ɸ��� �ð�
        float time = 0f;
        Color startColor = sprite.color;
        Color targetColor = Color.black;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            sprite.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        sprite.color = targetColor;
    }
}