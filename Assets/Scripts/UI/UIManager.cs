using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.Collections;
using static UnityEditor.PlayerSettings;

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
    Pause,
    ColorChange
}

public enum DungeonState
{
    Min = -1, 

    Dungeon1, //0
    Dungeon2, //1

    Max //enum StageState�� ����
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
    DungeonClearUI dungeonClearUI = null;
    ChestRewardUI chestRewardUI = null;
    BossRewardUI bossRewardUI = null;
    PauseUI pauseUI = null;
    ChangeColorUI changeColorUI = null;

    GameObject Dungeon1 = null;

    GameObject Dungeon2 = null;

    static UIManager instance;

    private IEnumerator coroutine;

    public bool isComplete = false; // ��ų ���� �Ϸ� Ȯ�ο�
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
        changeColorUI = GetComponentInChildren<ChangeColorUI>(true);
        changeColorUI?.Init(this);

        Dungeon1 = transform.Find("LobbyUI").transform.Find("Dungeon1").gameObject; //stage1�� stage2 ui ������Ʈ�� ã���༭ �Ҵ�
        Dungeon2 = transform.Find("LobbyUI").transform.Find("Dungeon2").gameObject; //transform.Find�� ã�� �� �ִ� �� ����Ʈ
 
        ChangeState(UIState.Title);
    }


    public void ChangeState(UIState state) //UI������Ʈ�� on off ���ִ� ���
    {
        currentState = state; //�Ʒ����� �ش��ϴ� UI������Ʈ�� ã�� on off ����
        titleUI?.SetActive(currentState); 
        lobbyUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        levelUpUI?.SetActive(currentState);
        gameOverUI?.SetActive(currentState);
        dungeonClearUI?.SetActive(currentState);
        chestRewardUI?.SetActive(currentState);
        bossRewardUI?.SetActive(currentState);
        pauseUI?.SetActive(currentState);
        changeColorUI?.SetActive(currentState);
    }

    public void ChangeDungeonState(DungeonState state) //�Ʒ����� �ش��ϴ� stage�� ã�Ƽ� on off ����
    {
        dungeonState = state; //�ش��ϴ� stage�� ã�Ƽ� ���� �־���


        switch(dungeonState)
        {
            case DungeonState.Dungeon1: //���� ���������� ��������1�� ���
                Dungeon1.SetActive(true);
                Dungeon2.SetActive(false);
                break;
            case DungeonState.Dungeon2: //���� ���������� ��������2�� 
                Dungeon1.SetActive(false);
                Dungeon2.SetActive(true);
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

    public void OnClickDungeonStart() // �������� ���� ��ư�� ������ ��
    {
        UpdatePlayerDungeon();  //� ���������� �������� ������ ��
        ChangeState(UIState.Game); //������ ���۵����� ���� UI�� ����
        SceneManager.LoadScene("Stage_6-Box");
        GameManager.Instance.GameStart();
        //���������� �߰� �ȴٸ� �̺κ��� ������ ��
    }

    public void OnClickNextDungeon()
    {
        if(dungeonState < DungeonState.Max - 1) //���罺�������� �ִ� �������� �������� ���� ��쿡��
        {
            ChangeDungeonState(dungeonState + 1); //���� ����������
        }    
    }

    public void OnClickPrevDungeon()
    {
        if (dungeonState > DungeonState.Min + 1) //���罺�������� �ּ� ������������ ū ��쿡��
        {
            ChangeDungeonState(dungeonState - 1); //���� ����������
        }
    }

    public void OnClickChangeColor()
    {
        ChangeState(UIState.ColorChange);
    }

    public void OnClickChangeColorCancel()
    {
        ChangeState(UIState.Lobby);
    }

    //Game ����

    public void UpdatePlayerDungeon() //�÷��̾ �� ������������ Ȱ���ϴ� �� Ȯ�ο�
    {
        gameUI.SetDungeonUI(dungeonState);
    }

    public void UpdatePlayerUIPosition(Vector2 position) // �÷��̾� Ui ���� Ȯ�ο�
    {
        gameUI.SetPlayerUIPosition(position);
    }

    public void UpdatePlayerHP(float maxHp, int currentHp) //�÷��̾��� HP Ȯ�ο�
    {
        gameUI.SetPlayerHpUI(maxHp,currentHp);
    }

    public void UpdatePlayerExp(float maxExp, float currentExp) //�÷��̾��� ����ġ Ȯ�ο�
    {
        gameUI.SetPlayerExpUI(maxExp,currentExp);
    }

    public void OnClickPause() //pause ��ư�� ���� ���
    {
        Time.timeScale = 0.0f;
        ChangeState(UIState.Pause);
    }

    public void StageShift() //���������� ����Ǹ� �������� �̸��� �ٲٽÿ�
    {
        gameUI.ChangeStageName();
    }

    //LevelUp����

    public void LevelUpUI() //������ ��� ����
    {
        isComplete = false;
        coroutine = WaitAndPrint(0.5f); //��ȯ�� IEnumerator�� �����صд�.
        StartCoroutine(coroutine); //0.5�ʰ� ������ ������ ����
    }

    public void OnClickSkillSelected()
    {
        isComplete = true;
        Debug.Log("��ų���ÿϷ�");
        ChangeState(UIState.Game); //�������� ��ų ��� ������ ��ģ �� �ٽ� �ΰ��� ui on
    }

    //GameOver����

    public void GameOverUI() //���� ������ ���ӿ��� UI ȣ��
    {
        ChangeState(UIState.GameOver);
    }

    public void OnClickBackButton() //BackButton�� ������ �κ�� ����
    {
        Destroy(GameManager.Instance.player);
        Destroy(GameManager.Instance.playerManager);

        SceneManager.LoadScene("UIScene"); 
        ChangeState(UIState.Lobby);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); //waitTime ��ŭ �������� ���� �ڵ尡 ����ȴ�.
            levelUpUI.SkillSelectOn();
            ChangeState(UIState.LevelUp); //������ ui Ȱ��ȭ
            break;
        }
    }


    //Pause ����

    public void OnClickContinue() //����ؼ� ����
    {
        Time.timeScale = 1.0f;
        ChangeState(UIState.Game);
    }

    public void OnClickLobby() //���� ������ �κ�� �̵�
    {
        Time.timeScale = 1.0f;

        Destroy(GameManager.Instance.player);
        Destroy(GameManager.Instance.playerManager);

        SceneManager.LoadScene("UIScene");
        ChangeState(UIState.Lobby);
    }

}
