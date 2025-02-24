using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int level;
    public int Level { get { return level; } set { level = value; } }

    private float currentExp;
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }

    private float maxExp;
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }

    private int attackPower;
    public int AttackPower { get { return attackPower; } set { attackPower = value; } }

    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    private int maxHealth;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    public void InitPlayerStats()
    {
        level = 1; currentExp = 0; maxExp = 100; attackPower = 30; currentHealth = 200; maxHealth = 200; speed = 3f; attackSpeed = 1f;
    }
<<<<<<< HEAD
}
=======

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnDamaged(30);
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

        StartCoroutine(ApplyInvincibilityAfterDamage());
    }

    public void PlayerDead()
    {
        Debug.Log("�÷��̾� ���");
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
>>>>>>> parent of 57a8298 ([ Chore ] 테스트 코드 삭제)
