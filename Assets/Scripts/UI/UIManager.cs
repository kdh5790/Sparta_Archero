using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public enum UIState
{
    Title,
    Lobby
}

public enum StageState
{
    Stage1,
    Stage2
}

public class UIManager : MonoBehaviour
{
    UIState currentState = UIState.Title;
    StageState stageState = StageState.Stage1;

    TitleUI titleUI = null;

    LobbyUI lobbyUI = null;

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
        SceneManager.LoadScene("MainScene");
    }

}
