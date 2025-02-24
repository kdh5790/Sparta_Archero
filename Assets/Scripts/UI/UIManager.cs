using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public enum UIState
{
    Title, //0
    Lobby //1
}

public enum StageState
{
    Min = -1, 

    Stage1, //0
    Stage2, //1

    Max //enum StageState�� ����
}

public class UIManager : MonoBehaviour
{
    UIState currentState = UIState.Title;
    StageState stageState = StageState.Stage1;

    TitleUI titleUI = null;

    LobbyUI lobbyUI = null;

    GameObject stage1 = null;

    GameObject stage2 = null;

    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;

        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);

        stage1 = transform.Find("LobbyUI").transform.Find("Stage1").gameObject; //stage1�� stage2 ui ������Ʈ�� ã���༭ �Ҵ�
        stage2 = transform.Find("LobbyUI").transform.Find("Stage2").gameObject;


        ChangeState(UIState.Title);
    }


    public void ChangeState(UIState state) //UI������Ʈ�� on off ���ִ� ���
    {
        currentState = state; //�Ʒ����� �ش��ϴ� UI������Ʈ�� ã�� on off ����
        titleUI?.SetActive(currentState); 
        lobbyUI?.SetActive(currentState);
    }

    public void ChangeStageState(StageState state) //�Ʒ����� �ش��ϴ� stage�� ã�Ƽ� on off ����
    {
        stageState = state; //�ش��ϴ� stage�� ã�Ƽ� ���� �־���


        switch(stageState)
        {
            case StageState.Stage1: //���� ���������� ��������1�� ���
                stage1.SetActive(true);
                stage2.SetActive(false);
                break;
            case StageState.Stage2: //���� ���������� ��������2�� 
                stage1.SetActive(false);
                stage2.SetActive(true);
                break;
        }
    }


    //title ����

    public void OnClickStart()
    {
        ChangeState(UIState.Lobby); // Start ��ư�� ������ �κ�� �̵�
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }



    //lobby ����

    public void OnClickStageStart() // �������� ���� ��ư�� ������ ��
    {
        //���������� �߰� �ȴٸ� �̺κ��� ������ ��
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickNextStage()
    {
        if(stageState < StageState.Max - 1) //���罺�������� �ִ� �������� �������� ���� ��쿡��
        {
            ChangeStageState(stageState + 1); //���� ����������
        }    
    }

    public void OnClickPrevStage()
    {
        if (stageState > StageState.Min + 1) //���罺�������� �ּ� ������������ ū ��쿡��
        {
            ChangeStageState(stageState - 1); //���� ����������
        }
    }

}
