using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private string[] stageOrder = {
        "Stage_5-1", "Stage_5-2", "Stage_5-3", "Stage_5-4",
        "Stage_6-Box", "Stage_7-1", "Stage_7-2", "Stage_7-3", "Stage_7-4",
        "Stage_8-Boss2"
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
