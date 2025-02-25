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
    Game, //2
    LevelUp, //3
    GameOver //4
}

public enum DungeonState
{
    Min = -1, 

    Dungeon1, //0
    Dungeon2, //1

    Max //enum StageState의 개수
}

public class UIManager : MonoBehaviour
{
    UIState currentState = UIState.Title;
    DungeonState dungeonState = DungeonState.Dungeon1;

    TitleUI titleUI = null;
    LobbyUI lobbyUI = null;
    GameUI gameUI = null;   
    LevelUpUI levelUpUI = null;
    GameOverUI gameOverUI = null;

    GameObject Dungeon1 = null;

    GameObject Dungeon2 = null;

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


        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }


        instance = this;

        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        levelUpUI = GetComponentInChildren<LevelUpUI>(true);
        levelUpUI?.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI?.Init(this);

        Dungeon1 = transform.Find("LobbyUI").transform.Find("Dungeon1").gameObject; //stage1과 stage2 ui 오브젝트를 찾아줘서 할당
        Dungeon2 = transform.Find("LobbyUI").transform.Find("Dungeon2").gameObject; //transform.Find로 찾아 들어가 주는 게 포인트
 
        ChangeState(UIState.Title);
    }


    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌
        titleUI?.SetActive(currentState); 
        lobbyUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        levelUpUI?.SetActive(currentState);
        gameOverUI?.SetActive(currentState);
    }

    public void ChangeDungeonState(DungeonState state) //아래에서 해당하는 stage를 찾아서 on off 해줌
    {
        dungeonState = state; //해당하는 stage를 찾아서 값을 넣어줌


        switch(dungeonState)
        {
            case DungeonState.Dungeon1: //현재 스테이지가 스테이지1인 경우
                Dungeon1.SetActive(true);
                Dungeon2.SetActive(false);
                break;
            case DungeonState.Dungeon2: //현재 스테이지가 스테이지2인 
                Dungeon1.SetActive(false);
                Dungeon2.SetActive(true);
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

    public void OnClickDungeonStart() // 스테이지 실행 버튼을 누렀을 시
    {
        ChangeState(UIState.Game); //게임이 시작됐으니 게임 UI로 변경
        UpdatePlayerDungeon();  //어떤 스테이지를 눌렀는지 보여줄 것
        SceneManager.LoadScene("SampleScene");
        //스테이지가 추가 된다면 이부분을 수정할 것
    }

    public void OnClickNextDungeon()
    {
        if(dungeonState < DungeonState.Max - 1) //현재스테이지가 최대 스테이지 개수보다 적은 경우에만
        {
            ChangeDungeonState(dungeonState + 1); //다음 스테이지로
        }    
    }

    public void OnClickPrevDungeon()
    {
        if (dungeonState > DungeonState.Min + 1) //현재스테이지가 최소 스태이지보다 큰 경우에만
        {
            ChangeDungeonState(dungeonState - 1); //이전 스테이지로
        }
    }


    //Game 내부

    public void UpdatePlayerDungeon() //플레이어가 몇 스테이지에서 활동하는 지 확인용
    {
        gameUI.SetDungeonUI(dungeonState);
    }

    public void UpdatePlayerUIPosition(Vector2 position) // 플레이어 Ui 추적 확인용
    {
        gameUI.SetPlayerUIPosition(position);
    }

    public void UpdatePlayerHP(float maxHp, int currentHp) //플레이어의 HP 확인용
    {
        gameUI.SetPlayerHpUI(maxHp,currentHp);
    }

    public void UpdatePlayerExp(float maxExp, float currentExp) //플레이어의 경험치 확인용
    {
        gameUI.SetPlayerExpUI(maxExp,currentExp);
    }

    //LevelUp내부

    public void LevelUpUI() //레벨업 테스트용
    {
        levelUpUI.SkillSelectOn(); //스킬 선택창에 랜덤 스킬 삽입용
        ChangeState(UIState.LevelUp); //레벨업 ui 활성화
    }

    public void OnClickSkillSelected()
    {
        Debug.Log("스킬선택완료");
        ChangeState(UIState.Game); //레벨업시 스킬 얻는 과정을 거친 후 다시 인게임 ui on
    }

    //GameOver내부

    public void GameOverUI() //게임 오버시 게임오버 UI 호출
    {
        ChangeState(UIState.GameOver);
    }

    public void OnClickBackButton() //BackButton을 누르면 로비로 복귀
    {
        SceneManager.LoadScene("UIScene"); 
        ChangeState(UIState.Lobby);
    }

}
