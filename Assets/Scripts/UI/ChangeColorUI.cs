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
        redSlider.value = 1;
        redSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        greenSlider.value = 1;
        greenSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        blueSlider.value = 1;
        blueSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        applyBtn = transform.Find("ApplyButton").GetComponent<Button>();

        backBtn = transform.Find("BackButton").GetComponent<Button>();
        backBtn.onClick.AddListener(OnClickCancelButton);

        noticeObj.SetActive(false);
    }

    void UpdateColor()
    {
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        playerImage.color = newColor;
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