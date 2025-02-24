using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using static UnityEditor.PlayerSettings;

public enum UIState
{
    Title, //0
    Lobby, //1
    Game //2
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
    GameUI gameUI = null;   

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
        DontDestroyOnLoad(gameObject); //씬변경시 파괴 x


        instance = this;

        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        stage1 = transform.Find("LobbyUI").transform.Find("Stage1").gameObject; //stage1과 stage2 ui 오브젝트를 찾아줘서 할당
        stage2 = transform.Find("LobbyUI").transform.Find("Stage2").gameObject; //transform.Find로 찾아 들어가 주는 게 포인트
 
        ChangeState(UIState.Title);
    }


    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌
        titleUI?.SetActive(currentState); 
        lobbyUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
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
        ChangeState(UIState.Game); //게임이 시작됐으니 게임 UI로 변경
        UpdatePlayerStage();  //어떤 스테이지를 눌렀는지 보여줄 것
        SceneManager.LoadScene("SampleScene");
        //스테이지가 추가 된다면 이부분을 수정할 것
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


    //Game 내부

    public void UpdatePlayerStage() //플레이어가 몇 스테이지에서 활동하는 지 확인용
    {
        gameUI.SetStageUI(stageState);
    }

    public void UpdatePlayerUIPosition(Vector2 position) // 플레이어 Ui 추적 확인용
    {
        gameUI.SetPlayerUIPosition(position);
    }

    public void UpdatePlayerHP(float maxHp, int currentHp) //플레이어의 HP 확인용
    {
        gameUI.SetPlayerHpUI(maxHp,currentHp);
    }

    public void UpdatePlayerExp() //플레이어의 경험치 확인용
    {
        gameUI.SetPlayerExpUI();
    }

}
