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

        UIManager.Instance.UpdatePlayerHP(maxHealth, currentHealth); //플레이어 ui 업데이트용
        UIManager.Instance.UpdatePlayerExp(maxExp, currentExp); //플레이어 ui 업데이트용
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnDamaged(300);
        }

        if (Input.GetKeyDown(KeyCode.P)) //ui 테스트용입니다
        {
            IncreaseExp(30);
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
        UIManager.Instance.UpdatePlayerHP(maxHealth, currentHealth); //플레이어 ui 업데이트용

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

        UIManager.Instance.UpdatePlayerExp(maxExp, currentExp); //플레이어 ui 업데이트용

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

        UIManager.Instance.UpdatePlayerExp(maxExp, currentExp); //플레이어 ui 업데이트용
        UIManager.Instance.LevelUpUI();// 스킬 획득 UI ON
        Debug.Log($"레벨업! Lv.{level}, MaxExp:{maxExp}");
    }

    public void PlayerDead()
    {
        Debug.Log("플레이어 사망");
        PlayerManager.instance.isDead = true;
        StartCoroutine(PlayerSpriteColorChange());
        GetComponent<PlayerController>().animator.speed = 0;

        if (invincibilityCoroutine != null)
            StopCoroutine(invincibilityCoroutine);

        PlayerManager.instance.bow.StopBowSkillCoroutine();
    }

    // 데미지를 입은 후 무적판정
    public IEnumerator ApplyInvincibilityAfterDamage()
    {
        invincibilityCoroutine = ApplyInvincibilityAfterDamage();

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

    public IEnumerator PlayerSpriteColorChange()
    {
        Debug.Log("asdsa");

        float duration = 1f; // 색상 변경에 걸리는 시간
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