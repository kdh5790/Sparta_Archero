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
            Debug.Log($"무적 상태이므로 {damage}의 데미지를 입지 않습니다.");
            return;
        }

        if (Random.Range(0, 100) < DodgeChance)
        {
            Debug.Log("회피에 성공했습니다.");
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
        Debug.Log("플레이어 사망");
    }

    // 데미지를 입은 후 무적판정
    public IEnumerator ApplyInvincibilityAfterDamage()
    {
        IsInvincivility = true;

        int count = 3; // 깜빡일 횟수

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

    // 무적 스킬 보유 시 10초 마다 2초간 무적
    public IEnumerator ApplyInvincibilitySkill()
    {
        while (currentHealth > 0)
        {
            IsInvincivility = false;

            yield return new WaitForSeconds(10f);

            Debug.Log("무적 적용");
            IsInvincivility = true;
            sprite.color = new Color(0.25f, 0.35f, 1);

            yield return new WaitForSeconds(2f);

            Debug.Log("무적 해제");
            IsInvincivility = false;
            sprite.color = Color.white;
        }
    }
}
>>>>>>> parent of 57a8298 ([ Chore ] �뀒�뒪�듃 肄붾뱶 �궘�젣)
