using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public delegate void ShootArrowDelegate(GameObject target);

public class ArrowManager : MonoBehaviour
{
    public ShootArrowDelegate shootDelegate;

    [SerializeField] private GameObject arrowPrefab; // 화살 프리팹
    private Queue<GameObject> arrowQueue = new Queue<GameObject>();
    private GameObject target;

    public int poolSize = 20;

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
        shootDelegate(_target);
    }

    public void AddShootDelegate(Skill skill)
    {
        switch (skill)
        {
            case Skill.BackArrowPlus:
                shootDelegate += ShootBackArrow;
                break;
        }
    }

    private void ShootSingleArrow(GameObject _target)
    {
        target = _target;

        GameObject go = arrowQueue.Dequeue();

        float angle = LookAtTargetForArrow();

        go.SetActive(true);
        go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void ShootBackArrow(GameObject _target)
    {
        target = _target;

        GameObject go = arrowQueue.Dequeue();

        float angle = LookAtTargetForArrow();

        go.SetActive(true);
        go.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;
        arrow.transform.localScale = Vector3.one;

        arrowQueue.Enqueue(arrow);
        arrow.SetActive(false);
    }

    private float LookAtTargetForArrow()
    {
        // 바라보는 방향 구하기
        Vector2 direction = target.transform.position - transform.position;

        // 방향 각도 계산 후 라디안 값을 도 단위로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z 회전값으로 타겟 바라보기
        return angle;
    }


}
