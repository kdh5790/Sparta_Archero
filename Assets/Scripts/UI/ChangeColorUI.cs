using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorUI : BaseUI
{
    [SerializeField] private Slider redSlider = null;
    [SerializeField] private Slider greenSlider = null;
    [SerializeField] private Slider blueSlider = null;
    [SerializeField] private GameObject noticeObj = null;
    [SerializeField] private Image playerImage = null;

    private Button applyBtn = null;
    private Button backBtn = null;

    protected override UIState GetUIState()
    {
        return UIState.ColorChange;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        redSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        greenSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        blueSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        applyBtn = transform.Find("ApplyButton").GetComponent<Button>();
        applyBtn.onClick.AddListener(OnClickApplyButton);

        backBtn = transform.Find("BackButton").GetComponent<Button>();
        backBtn.onClick.AddListener(OnClickCancelButton);

        noticeObj.SetActive(false);

    }

    private void OnEnable()
    {
        playerImage.color = DataManager.Instance.LoadColor();
        redSlider.value = playerImage.color.r;
        greenSlider.value = playerImage.color.g;
        blueSlider.value = playerImage.color.b;
    }

    void UpdateColor()
    {
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        playerImage.color = newColor;
    }

    void OnClickApplyButton()
    {
        StartCoroutine(OnNoticeUI());

        if (DataManager.Instance == null)
        {
            Debug.Log("DataManager�� ã�� ���߽��ϴ�.");
            return; 
        }

        DataManager.Instance.SaveColor(playerImage.color);
    }

    void OnClickCancelButton()
    {
        uiManager.OnClickChangeColorCancel();
    }

    private IEnumerator OnNoticeUI()
    {
        noticeObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        noticeObj.SetActive(false);
    }
}