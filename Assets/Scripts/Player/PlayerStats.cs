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

    public void InitPlayerStats()
    {
        level = 1; currentExp = 0; maxExp = 100; currentHealth = 600; maxHealth = 600; speed = 3f;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
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

        if (currentHealth <= 0)
        {
            PlayerDead();
        }

        UIManager.Instance.UpdatePlayerHP(maxHealth,currentHealth); //�÷��̾� ui ������Ʈ��

        StartCoroutine(ApplyInvincibilityAfterDamage());
    }

    public void IncreaseExp(int exp)
    {
        currentExp += exp;

        if(currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= maxExp;
        maxExp *= 1.2f;

        level++;

        UIManager.Instance.LevelUpUI();// ��ų ȹ�� UI ON
        Debug.Log($"������! Lv.{level}, MaxExp:{maxExp}");
    }

    public void PlayerDead()
    {
        Debug.Log("�÷��̾� ���");
        PlayerManager.instance.isDead = true;
    }

    // �������� ���� �� ��������
    public IEnumerator ApplyInvincibilityAfterDamage()
    {
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
            IsInvincivility = false;

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


}