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

    Max //enum StageState의 개수
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

        stage1 = transform.Find("LobbyUI").transform.Find("Stage1").gameObject; //stage1과 stage2 ui 오브젝트를 찾아줘서 할당
        stage2 = transform.Find("LobbyUI").transform.Find("Stage2").gameObject;


        ChangeState(UIState.Title);
    }


    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌
        titleUI?.SetActive(currentState); 
        lobbyUI?.SetActive(currentState);
    }

    public void ChangeStageState(StageState state) //아래에서 해당하는 stage를 찾아서 on off 해줌
    {
        stageState = state; //해당하는 stage를 찾아서 값을 넣어줌


        switch(stageState)
        {
            case StageState.Stage1: //현재 스테이지가 스테이지1인 경우
                stage1.SetActive(true);
                stage2.SetActive(false);
                break;
            case StageState.Stage2: //현재 스테이지가 스테이지2인 
                stage1.SetActive(false);
                stage2.SetActive(true);
                break;
        }
    }


    //title 내부

    public void OnClickStart()
    {
        ChangeState(UIState.Lobby); // Start 버튼을 누르면 로비로 이동
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }



    //lobby 내부

    public void OnClickStageStart() // 스테이지 실행 버튼을 누렀을 시
    {
        //스테이지가 추가 된다면 이부분을 수정할 것
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickNextStage()
    {
        if(stageState < StageState.Max - 1) //현재스테이지가 최대 스테이지 개수보다 적은 경우에만
        {
            ChangeStageState(stageState + 1); //다음 스테이지로
        }    
    }

    public void OnClickPrevStage()
    {
        if (stageState > StageState.Min + 1) //현재스테이지가 최소 스태이지보다 큰 경우에만
        {
            ChangeStageState(stageState - 1); //이전 스테이지로
        }
    }

}
