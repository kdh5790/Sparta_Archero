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
}
