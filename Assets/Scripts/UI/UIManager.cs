using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.Collections;

public enum UIState
{
    Title, //0
    Lobby, //1
    Game, //2
    LevelUp, //3
    GameOver, //4
    DungeonClear, //5
    ChestReward, //6
    BossReward, //7
    Pause, //8
    Setting, //9
    ColorChange //10
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
    UIState prevState = UIState.Title;
    DungeonState dungeonState = DungeonState.Dungeon1;

    TitleUI titleUI = null;
    LobbyUI lobbyUI = null;
    GameUI gameUI = null;   
    LevelUpUI levelUpUI = null;
    GameOverUI gameOverUI = null;
    DungeonClearUI dungeonClearUI = null;
    ChestRewardUI chestRewardUI = null;
    BossRewardUI bossRewardUI = null;
    PauseUI pauseUI = null;
    SettingUI settingUI = null;
    ChangeColorUI changeColorUI = null;

    GameObject Dungeon1 = null;

    GameObject Dungeon2 = null;

    static UIManager instance;

    private IEnumerator coroutine;

    public bool isComplete = false; // 스킬 선택 완료 확인용
    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {


        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }


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
        dungeonClearUI = GetComponentInChildren<DungeonClearUI>(true);
        dungeonClearUI?.Init(this);
        chestRewardUI = GetComponentInChildren<ChestRewardUI>(true);
        chestRewardUI?.Init(this);
        bossRewardUI = GetComponentInChildren<BossRewardUI>(true);
        bossRewardUI?.Init(this);
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI?.Init(this);
        settingUI = GetComponentInChildren<SettingUI>(true);
        settingUI?.Init(this);
        changeColorUI = GetComponentInChildren<ChangeColorUI>(true);
        changeColorUI?.Init(this);

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
        dungeonClearUI?.SetActive(currentState);
        chestRewardUI?.SetActive(currentState);
        bossRewardUI?.SetActive(currentState);
        pauseUI?.SetActive(currentState);
        settingUI?.SetActive(currentState);
        changeColorUI.SetActive(currentState);
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
        if (DataManager.Instance.LoadBoxOpen() >= 1) //이미 상자 도전 과제를 달성했다면, 항상 열어둬라
        {
            lobbyUI.changeColorObject.SetActive(true);
        }

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
        UpdatePlayerDungeon();  //어떤 스테이지를 눌렀는지 보여줄 것
        ChangeState(UIState.Game); //게임이 시작됐으니 게임 UI로 변경

        SoundManager.instance.PlayMusic(BGM.Dungeon);

        if (dungeonState == DungeonState.Dungeon1)
        {
            SceneManager.LoadScene("Stage_1");
        }

        if(dungeonState == DungeonState.Dungeon2)
        {
            SceneManager.LoadScene("Stage_11");
        }

        GameManager.Instance.GameStart();
        //스테이지가 추가 된다면 이부분을 수정할 것
    }

    public void OnClickTitle()
    {
        ChangeState(UIState.Title);
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

    public void BoxAchievementReward()
    {
        lobbyUI.BoxAchievementCheck();
    }

    public void OnClickChangeColor()
    {
        ChangeState(UIState.ColorChange);
    }

    public void OnClickChangeColorCancel()
    {
        ChangeState(UIState.Lobby);
    }

    //Game 내부

    public void UpdateStageName()
    {
        gameUI.ChangeStageName();
    }

    public void UpdateClearTime(float time)
    {
        gameUI.UpdateClearTimeText(time);
    }

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

    public void OnClickPause() //pause 버튼을 누른 경우
    {
        Time.timeScale = 0.0f;
        ChangeState(UIState.Pause);
    }

    public void StageShift() //스테이지가 변경되면 스테이지 이름을 바꾸시오
    {
        gameUI.ChangeStageName();
    }

    //LevelUp내부

    public void LevelUpUI() //레벨업 기능 구현
    {
        if (PlayerManager.instance.isDead != true) //플레이어가 죽은 경우 레벨업 ui 호출 불가
        {
            isComplete = false;
            coroutine = LevelUpDelay(1.0f); //반환값 IEnumerator를 저장해둔다.
            StartCoroutine(coroutine); //1초가 지난후 레벨업 시작
        }
    }

    public void OnClickSkillSelected()
    {
        isComplete = true;
        Debug.Log("스킬선택완료");
        ChangeState(UIState.Game); //레벨업시 스킬 얻는 과정을 거친 후 다시 인게임 ui on
    }

    private IEnumerator LevelUpDelay(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
            levelUpUI.SkillSelectOn();
            ChangeState(UIState.LevelUp);
            break;
        }
    }

    //GameOver내부

    public void GameOverUI() //게임 오버시 게임오버 UI 호출
    {
        ChangeState(UIState.GameOver);
    }

    public void OnClickBackButton() //BackButton을 누르면 로비로 복귀
    {
        Destroy(GameManager.Instance.player);
        Destroy(GameManager.Instance.playerManager);

        SceneManager.LoadScene("UIScene"); 
        ChangeState(UIState.Lobby);
    }


    //Pause 내부

    public void OnClickContinue() //계속해서 게임
    {
        Time.timeScale = 1.0f;
        ChangeState(UIState.Game);
    }

    public void OnClickLobby() //게임 종료후 로비로 이동
    {
        Time.timeScale = 1.0f;

        Destroy(GameManager.Instance.player);
        Destroy(GameManager.Instance.playerManager);

        SceneManager.LoadScene("UIScene");
        ChangeState(UIState.Lobby);
    }

    //ChestReward 내부

    public void ChestTriggered()
    {
        isComplete = false;
        coroutine = ChestRewardDelay(1.0f);
        StartCoroutine(coroutine);
    }

    public void OnClickChestSelected()
    {
        isComplete = true;
        ChangeState(UIState.Game);
        Debug.Log("스킬선택완료");
    }

    private IEnumerator ChestRewardDelay(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
            ChangeState(UIState.ChestReward);
            chestRewardUI.SkillSelectOn();
            break;
        }
    }
    
    //Setting 내부

    public void OnClickSetting()
    {
        prevState = currentState; //Setiing에서 Back 버튼을 누르면 이전 ui로 돌아가게 하기 위함
        ChangeState(UIState.Setting);
    }

    public void OnClickBackSetting()
    {
        ChangeState(prevState);
    }

    //BossReward 내부
    public void GetBossReward()
    {
        isComplete = false;
        coroutine = BossRewardDelay(0.8f);
        StartCoroutine(coroutine);
    }
    public void OnClickBossSelected()
    {
        isComplete = true;
        ChangeState(UIState.Game);
    }
    private IEnumerator BossRewardDelay(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
            ChangeState(UIState.BossReward);
            bossRewardUI.SkillSelectOn();
            break;
        }
    }

    //DungeonClear 내부

    public void CallDungeonClear(float time, int level)
    {
        coroutine = DungeonClearDelay(0.8f, time, level);
        StartCoroutine(coroutine);
    }

    public void BackToLobby()
    {
        Destroy(GameManager.Instance.player);
        Destroy(GameManager.Instance.playerManager);

        SceneManager.LoadScene("UIScene");
        ChangeState(UIState.Lobby);
    }

    private IEnumerator DungeonClearDelay(float waitTime, float time, int level)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
            ChangeState(UIState.DungeonClear);
            dungeonClearUI.UpdateScoreBoard(time,level);
            break;
        }
    }
}
