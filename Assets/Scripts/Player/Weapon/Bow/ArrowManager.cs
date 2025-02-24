using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public delegate void ShootArrowDelegate();

public class ArrowManager : MonoBehaviour
{
    public ShootArrowDelegate shootDelegate;

    [SerializeField] private GameObject arrowPrefab; // 화살 프리팹
    private Queue<GameObject> arrowQueue = new Queue<GameObject>(); // 생성한 화살들을 담아둘 큐
    private GameObject target;
    private Weapon_Bow bow;

    private int poolSize = 100; // 오브젝트풀 사이즈(생성할 화살 갯수)

    private void Start()
    {
        bow = FindObjectOfType<Weapon_Bow>();

        GameObject go;

        // 화살 생성 후 비활성화, 큐에 넣어주기
        for (int i = 0; i < poolSize; i++)
        {
            go = Instantiate(arrowPrefab, transform);
            go.SetActive(false);
            arrowQueue.Enqueue(go);
        }

        // 델리게이트에 기본공격 추가
        shootDelegate = ShootSingleArrow;
    }

    // 화살 발사
    public void StartShootDelegate(GameObject _target)
    {
        target = _target;

        if (bow.IsMultiShot)
            StartCoroutine(ShootMultiShotCoroutine());
        else
            shootDelegate();
    }

    // 스킬에 따라 델리게이트 추가
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

    // 스킬에 맞게 각도 변경 후 발사
    public void ShootArrow(float offset)
    {
        GameObject go = arrowQueue.Dequeue();

        float angle = LookAtTargetForArrow();

        go.SetActive(true);
        go.transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }

    // 기본 공격
    private void ShootSingleArrow()
    {
        ShootArrow(-90f);
    }

    // 후방 화살
    private void ShootBackArrow()
    {
        ShootArrow(90f);
    }

    // 사선 화살
    private void ShootDiagonalArrow()
    {
        ShootArrow(-45f);
        ShootArrow(-135f);
    }

    // 측면 화살
    private void ShootSideArrow()
    {
        ShootArrow(0f);
        ShootArrow(180f);
    }

    // 전방 화살
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

    // 화살 회수
    public void ReturnArrow(GameObject arrow)
    {
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;
        arrow.transform.localScale = Vector3.one;

        arrowQueue.Enqueue(arrow);
        arrow.SetActive(false);
    }

    // 화살을 타겟방향으로 회전시켜야 할 z값 구하기 (반동을 위한 방향을 구한다면 매개변수 넣어주기)
    public float LookAtTargetForArrow(GameObject _target = null, Transform _transform = null)
    {
        Vector2 direction;

        // 바라보는 방향 구하기
        if (_target == null)
            direction = target.transform.position - transform.position;

        else
            direction = _target.transform.position - _transform.position;

        // 방향 각도 계산 후 라디안 값을 도 단위로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z 회전값으로 타겟 바라보기
        return angle;
    }
}