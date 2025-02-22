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

    private int DodgeChance;
    public int dodgeChance { get { return DodgeChance; } set { DodgeChance = value; } }

    private bool IsInvincivility;
    public bool isInvincivility { get { return IsInvincivility; } set { IsInvincivility = value; } }

    private SpriteRenderer sprite;

    public void InitPlayerStats()
    {
        level = 1; currentExp = 0; maxExp = 100; currentHealth = 200; maxHealth = 200; speed = 3f;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnDamaged(int damage)
    {
        if (IsInvincivility)
        {
            Debug.Log($"무적 상태이므로 {damage}의 데미지를 입지 않습니다.");
            return;
        }

        currentHealth -= damage;
        Mathf.Min(0, currentHealth);
    }

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
