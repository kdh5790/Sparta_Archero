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
            Debug.Log("���� ������ �����ϴ� �� ������ �߻��߽��ϴ�."); 
            return;
        }

        Debug.Log($"{color.r}, {color.g}, {color.b} ������ �����߽��ϴ�.");
    }

    public Color LoadColor()
    {
        Color loadedColor = Color.white;

        try
        {
            string loadedColorStirng = PlayerPrefs.GetString(ColorKey, string.Empty);

            if (ColorUtility.TryParseHtmlString("#" + loadedColorStirng, out loadedColor))
            {
                Debug.Log($"{loadedColor.r}, {loadedColor.g}, {loadedColor.b} ������ �ҷ��Խ��ϴ�.");
                return loadedColor;
            }
        }
        catch
        {
            Debug.LogError("���� ������ �ҷ����� �� ������ �߻��߽��ϴ�."); return Color.white;
        }

        return loadedColor;
    }
}
