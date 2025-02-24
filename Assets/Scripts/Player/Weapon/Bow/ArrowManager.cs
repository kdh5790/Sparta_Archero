using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public delegate void ShootArrowDelegate();

public class ArrowManager : MonoBehaviour
{
    public ShootArrowDelegate shootDelegate;

    [SerializeField] private GameObject arrowPrefab; // ȭ�� ������
    private Queue<GameObject> arrowQueue = new Queue<GameObject>(); // ������ ȭ����� ��Ƶ� ť
    private GameObject target;
    private Weapon_Bow bow;

    private int poolSize = 100; // ������ƮǮ ������(������ ȭ�� ����)

    private void Start()
    {
        bow = FindObjectOfType<Weapon_Bow>();

        GameObject go;

        // ȭ�� ���� �� ��Ȱ��ȭ, ť�� �־��ֱ�
        for (int i = 0; i < poolSize; i++)
        {
            go = Instantiate(arrowPrefab, transform);
            go.SetActive(false);
            arrowQueue.Enqueue(go);
        }

        // ��������Ʈ�� �⺻���� �߰�
        shootDelegate = ShootSingleArrow;
    }

    // ȭ�� �߻�
    public void StartShootDelegate(GameObject _target)
    {
        target = _target;

        if (bow.IsMultiShot)
            StartCoroutine(ShootMultiShotCoroutine());
        else
            shootDelegate();
    }

    // ��ų�� ���� ��������Ʈ �߰�
    public void AddShootDelegate(Skill skill)
    {
        switch (skill)
        {
            case Skill.FrontArrowPlus:
                shootDelegate += ShootFrontArrow;
                break;

            case Skill.BackArrowPlus:
                shootDelegate += ShootBackArrow;
                break;

            case Skill.DiagonalArrowPlus:
                shootDelegate += ShootDiagonalArrow;
                break;

            case Skill.SideArrowPlus:
                shootDelegate += ShootSideArrow;
                break;
        }
    }

    // ��ų�� �°� ���� ���� �� �߻�
    public void ShootArrow(float offset)
    {
        GameObject go = arrowQueue.Dequeue();

        float angle = LookAtTargetForArrow();

        go.SetActive(true);
        go.transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }

    // �⺻ ����
    private void ShootSingleArrow()
    {
        ShootArrow(-90f);
    }

    // �Ĺ� ȭ��
    private void ShootBackArrow()
    {
        ShootArrow(90f);
    }

    // �缱 ȭ��
    private void ShootDiagonalArrow()
    {
        ShootArrow(-45f);
        ShootArrow(-135f);
    }

    // ���� ȭ��
    private void ShootSideArrow()
    {
        ShootArrow(0f);
        ShootArrow(180f);
    }

    // ���� ȭ��
    private void ShootFrontArrow()
    {
        ShootArrow(-85f);
    }

    private IEnumerator ShootMultiShotCoroutine()
    {
        shootDelegate();

        yield return new WaitForSeconds(0.1f);
        shootDelegate();
    }

    // ȭ�� ȸ��
    public void ReturnArrow(GameObject arrow)
    {
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;
        arrow.transform.localScale = Vector3.one;

        arrowQueue.Enqueue(arrow);
        arrow.SetActive(false);
    }

    // ȭ���� Ÿ�ٹ������� ȸ�����Ѿ� �� z�� ���ϱ� (�ݵ��� ���� ������ ���Ѵٸ� �Ű����� �־��ֱ�)
    public float LookAtTargetForArrow(GameObject _target = null, Transform _transform = null)
    {
        Vector2 direction;

        // �ٶ󺸴� ���� ���ϱ�
        if (_target == null)
            direction = target.transform.position - transform.position;

        else
            direction = _target.transform.position - _transform.position;

        // ���� ���� ��� �� ���� ���� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z ȸ�������� Ÿ�� �ٶ󺸱�
        return angle;
    }
}