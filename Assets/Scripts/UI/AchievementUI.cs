using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    private int temp = 0;
    public TextMeshProUGUI AchievementTxt;
    public Animator animator;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        temp = DataManager.Instance.LoadBoxOpen();
        AchievementTxt = transform.Find("AchievementDescription").GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(temp != DataManager.Instance.LoadBoxOpen())
        {
            temp = DataManager.Instance.LoadBoxOpen();
            AchievementTxt.text = $"�ڽ��� {temp}��° �������ϴ�.";
            animator.SetInteger("step", 1);

            coroutine = Delay(2.0f);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator Delay(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime ��ŭ �������� ���� �ڵ尡 ����ȴ�.
            animator.SetInteger("step", 2);
            break;
        }
    }
}
