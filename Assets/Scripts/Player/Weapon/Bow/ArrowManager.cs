using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab; // È­»ì ÇÁ¸®ÆÕ
    private Queue<GameObject> arrowQueue = new Queue<GameObject>();

    public int poolSize = 20;

    private void Start()
    {
        GameObject go = new GameObject();

        for (int i = 0; i < poolSize; i++)
        {
            go = Instantiate(arrowPrefab, transform);
            go.SetActive(false);
            arrowQueue.Enqueue(go);
        }
    }

    public void ShootArrow()
    {
        GameObject go = arrowQueue.Dequeue();

        go.SetActive(true);
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;
        arrow.transform.localScale = Vector3.one;

        arrowQueue.Enqueue(arrow);
        arrow.SetActive(false);
    }
}
