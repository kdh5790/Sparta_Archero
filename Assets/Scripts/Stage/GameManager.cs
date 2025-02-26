using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public GameObject playerManagerPrefab;

    public GameObject player;
    public GameObject playerManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GameStart()
    {
        player = Instantiate(playerPrefab);
        playerManager = Instantiate(playerManagerPrefab);

        player.GetComponentInChildren<SpriteRenderer>().color = DataManager.Instance.LoadColor();

        playerPrefab.GetComponent<PlayerStats>().InitPlayerStats();
    }
}
