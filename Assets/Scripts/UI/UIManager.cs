using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    Title,
    Lobby
}

public class UIManager : MonoBehaviour
{
    UIState currentState = UIState.Title;

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


    public void ChangeState(UIState state)
    {
        currentState = state;
        titleUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Title);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
