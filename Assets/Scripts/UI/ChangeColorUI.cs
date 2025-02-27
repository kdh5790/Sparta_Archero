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
        // UI 활성화 시 저장된 Color 값을 불러온 후 슬라이드 값, 플레이어 이미지에 반영
        Color loadColor = DataManager.Instance.LoadColor();

        redSlider.value = loadColor.r;

        greenSlider.value = loadColor.g;

        blueSlider.value = loadColor.b;

        UpdateColor();
    }

    // 슬라이드 값 변경 마다 실행
    void UpdateColor()
    {
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        playerImage.color = newColor;
    }

    void OnClickApplyButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        StartCoroutine(OnNoticeUI());

        if (DataManager.Instance == null)
        {
            Debug.Log("DataManager를 찾지 못했습니다.");
            return; 
        }

        // 저장 버튼 클릭 시 PlayerPrefs에 현재 Color값 저장
        DataManager.Instance.SaveColor(playerImage.color);
    }

    void OnClickCancelButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickChangeColorCancel();
    }

    // 색상 저장 성공 여부 확인 알림 UI
    private IEnumerator OnNoticeUI()
    {
        noticeObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        noticeObj.SetActive(false);
    }
}