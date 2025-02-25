using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Weapon_Bow : MonoBehaviour
{
    private int damage = 200;
    public int Damage { get { return damage; } set { damage = value; } }

    private float attackSpeed = 1f;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    private float criticalDamage = 2f;
    public float CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }

    private int criticalChance = 10;
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    private bool isPiercingShot = false;
    public bool IsPiercingShot { get { return isPiercingShot; } set { isPiercingShot = value; } }

    private bool isRebound = false;
    public bool IsRebound { get { return isRebound; } set { isRebound = value; } }

    private bool isMultiShot = false;
    public bool IsMultiShot { get { return isMultiShot; } set { isMultiShot = value; } }

    private bool isRage = false;
    public bool IsRage { get { return isRage; } set { isRage = value; } }

    public List<GameObject> enemyList = new List<GameObject>(); // �ʵ��� ������ ���� ����Ʈ
    public GameObject target; // �����ؾ� �� Ÿ��

    [SerializeField] private SpriteRenderer attakSpeedAuroraSprite;
    [SerializeField] private SpriteRenderer criticalAuroraSprite;
    private Animator animator;
    private PlayerController playerController;

    private IEnumerator[] skillCoroutineArr = new IEnumerator[2];

    void Start()
    {
        animator = GetComponent<Animator>();

        playerController = GetComponentInParent<PlayerController>();

        UpdateEnemyList();
        target = FindTarget();
    }

    void Update()
    {
        if (playerController.isMove || target == null)
            target = FindTarget();

        LookAtTarget();

        // �÷��̾ �������� + Ÿ���� ���� + ���߰� �����ð��� ����
        if (!playerController.isMove && target != null && playerController.stopTime > 0.2f && !PlayerManager.instance.isDead)
            animator.SetBool("IsAttack", true);
        else
            animator.SetBool("IsAttack", false);
    }

    // ���� Ȱ��ȭ �� ���� ã�ƿ���                 �±װ� "Enemy"�̰� Ȱ��ȭ �� ������ ã�� ����Ʈ�� ��ȯ
    public void UpdateEnemyList() => enemyList = FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemy") && x.activeSelf).ToList();


    // ���� ����� �� ã��
    public GameObject FindTarget(Transform _transform = null, GameObject _enemy = null)
    {
        if (target != null && !target.activeSelf || target == null)
            UpdateEnemyList();


        GameObject go = null;

        if (enemyList != null)
        {
            float targetDistance = 100f;

            foreach (GameObject obj in enemyList)
            {
                // ȭ���� ����Ÿ���� ã�� ��� ���� �ڽ�(ȭ��)�� Ÿ���� �Ѿ��
                if (_enemy != null && obj.name == _enemy.name)
                    continue;

                float distance = 0;

                // �÷��̾�� ���� ����� �� ã��
                if (_transform == null)
                    distance = Vector3.Distance(obj.transform.position, transform.position);

                // �ݵ� ��ų ���� �� ȭ���� ���� Ÿ�� ã��
                else
                    distance = Vector3.Distance(obj.transform.position, _transform.position);

                if (distance < targetDistance)
                {
                    targetDistance = distance;
                    go = obj;
                }
            }
        }
        else
        {
            return null;
        }

        return go;
    }

    // Ÿ�� �ٶ󺸱�
    private void LookAtTarget()
    {
        if (target == null) return;
        // �ٶ󺸴� ���� ���ϱ�
        Vector2 direction = target.transform.position - transform.position;

        // ���� ���� ��� �� ���� ���� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z ȸ�������� Ÿ�� �ٶ󺸱�
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ����(�ִϸ��̼� �̺�Ʈ�� ȣ��)
    private void Attack()
    {
        if (target != null)
            PlayerManager.instance.arrowManager.StartShootDelegate(target);
    }

    // ���ݼӵ� ����
    public void IncreasedAttackSpeed(float speed)
    {
        AttackSpeed += speed;
        animator.speed = AttackSpeed;
    }

    // ũ��Ƽ�� Ȯ��
    public bool CalculateCriticalChance()
    {
        int randNum = UnityEngine.Random.Range(0, 100);

        return randNum < criticalChance;
    }

    public IEnumerator ApplyAttackSpeedAurora()
    {
        skillCoroutineArr[0] = ApplyAttackSpeedAurora();

        while (PlayerManager.instance.stats.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(9f);

            attakSpeedAuroraSprite.gameObject.SetActive(true);
            IncreasedAttackSpeed(0.625f);

            yield return new WaitForSeconds(2f);

            attakSpeedAuroraSprite.gameObject.SetActive(false);
            IncreasedAttackSpeed(-0.625f);
        }
    }

    public IEnumerator ApplyCriticalAurora()
    {
        skillCoroutineArr[1] = ApplyCriticalAurora();

        while (PlayerManager.instance.stats.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(9f);

            criticalAuroraSprite.gameObject.SetActive(true);
            criticalChance += 47;

            yield return new WaitForSeconds(2f);

            criticalAuroraSprite.gameObject.SetActive(false);
            criticalChance -= 47;
        }
    }

    // ��ų �ڷ�ƾ �ߴ�(��� �� ȣ��)
    public void StopBowSkillCoroutine()
    {
        foreach (var skillCoroutine in skillCoroutineArr)
        {
            if(skillCoroutine != null)
                StopCoroutine(skillCoroutine);
        }
    }
}