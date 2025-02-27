using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public TextMeshProUGUI AchievementTxt;
    public Animator animator;
    private IEnumerator coroutine;

    private bool isBoxAchievementClear = false;
    // Start is called before the first frame update
    void Start()
    {
        AchievementTxt = transform.Find("AchievementDescription").GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.Instance.LoadBoxOpen() >= 1 && !isBoxAchievementClear)
        {
            SoundManager.instance.PlaySound(SFX.LevelUp);
            isBoxAchievementClear = true;
            UIManager.Instance.BoxAchievementReward(); //도전과제 달성 보상
            AchievementTxt.text = $"첫 상자 개봉 - 캐릭터 커스텀 해제";
            animator.SetInteger("step", 1);

            coroutine = Delay(3.0f);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator Delay(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
            animator.SetInteger("step", 2);
            break;
        }
    }
}
