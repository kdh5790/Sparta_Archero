using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    PlayerStats stats;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        stats.InitPlayerStats();
    }
}
