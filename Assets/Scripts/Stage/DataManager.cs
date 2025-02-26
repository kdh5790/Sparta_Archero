using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public readonly string ColorKey = "SaveColor";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveColor(Color color)
    {
        try
        {
            PlayerPrefs.SetString(ColorKey, ColorUtility.ToHtmlStringRGBA(color));
            PlayerPrefs.Save();
        }
        catch 
        {
            Debug.Log("색상 정보를 저장하는 중 오류가 발생했습니다."); 
            return;
        }

        Debug.Log($"{color.r}, {color.g}, {color.b} 색상을 저장했습니다.");
    }

    public Color LoadColor()
    {
        Color loadedColor = Color.white;

        try
        {
            string loadedColorStirng = PlayerPrefs.GetString(ColorKey, string.Empty);

            if (ColorUtility.TryParseHtmlString("#" + loadedColorStirng, out loadedColor))
            {
                Debug.Log($"{loadedColor.r}, {loadedColor.g}, {loadedColor.b} 색상을 불러왔습니다.");
                return loadedColor;
            }
        }
        catch
        {
            Debug.LogError("색상 정보를 불러오는 중 오류가 발생했습니다."); return Color.white;
        }

        return loadedColor;
    }
}
