using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public delegate void ShootArrowDelegate();

public class ArrowManager : MonoBehaviour
{
    public ShootArrowDelegate shootDelegate;

    [SerializeField] private GameObject arrowPrefab; // 화살 프리팹
    private Queue<GameObject> arrowQueue = new Queue<GameObject>();
    private GameObject target;

    private int poolSize = 100;

    private void Start()
    {
        GameObject go;

        for (int i = 0; i < poolSize; i++)
        {
            go = Instantiate(arrowPrefab, transform);
            go.SetActive(false);
            arrowQueue.Enqueue(go);
        }

        shootDelegate = ShootSingleArrow;
    }

    public void StartShootDelegate(GameObject _target)
    {
        target = _target;
        shootDelegate();
    }

    public void AddShootDelegate(Skill skill)
    {
        switch (skill)
        {
            case Skill.BackArrowPlus:
                shootDelegate += ShootBackArrow;
                break;

            case Skill.DiagonalArrowPlus:
                shootDelegate += ShootDiagonalArrow;
                break;

            case Skill.SideArrowPlus:
                shootDelegate += ShootSideArrow;
                break;

            case Skill.MultiShot:
                shootDelegate += ShootMultiArrow;
                break;
        }
    }


    public void ShootArrow(float offset)
    {
        GameObject go = arrowQueue.Dequeue();

        float angle = LookAtTargetForArrow();

        go.SetActive(true);
        go.transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }

    private void ShootSingleArrow()
    {
        ShootArrow(-90f);
    }

    private void ShootBackArrow()
    {
        ShootArrow(90f);
    }

    private void ShootDiagonalArrow()
    {
        ShootArrow(-45f);
        ShootArrow(-135f);
    }

    private void ShootSideArrow()
    {
        ShootArrow(0f);
        ShootArrow(180f);
    }

    private void ShootMultiArrow()
    {
        GameObject go = arrowQueue.Dequeue();

        float angle = LookAtTargetForArrow();

        go.SetActive(true);
        go.transform.position = new Vector2(go.transform.position.x, go.transform.position.y - 0.15f);
        go.transform.rotation = Quaternion.Euler(0, 0, angle + -90f);
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;
        arrow.transform.localScale = Vector3.one;

        arrowQueue.Enqueue(arrow);
        arrow.SetActive(false);
    }

    private float LookAtTargetForArrow(GameObject _target = null)
    {
        Vector2 direction;

        // 바라보는 방향 구하기
        if (_target == null)
            direction = target.transform.position - transform.position;

        else
            direction = _target.transform.position - transform.position;


        // 방향 각도 계산 후 라디안 값을 도 단위로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z 회전값으로 타겟 바라보기
        return angle;
    }


}
