using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private string[] stageOrder = {
        "Stage_1", "Stage_2", "Stage_3", "Stage_4",
        "Stage_5","Stage_6", "Stage_7", "Stage_8", "Stage_9",
        "Stage_Boss", 
        "Stage_11", "Stage_12", "Stage_13", "Stage_14",
        "Stage_15", "Stage_16", "Stage_17", "Stage_18", "Stage_19",
        "Stage_Last Boss"
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int index = System.Array.IndexOf(stageOrder, currentScene);

            if (index != -1 && index < stageOrder.Length - 1)
            {
                SceneManager.LoadScene(stageOrder[index + 1]); // 다음 스테이지 로드
            }
        }
    }
}
