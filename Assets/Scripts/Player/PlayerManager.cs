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

        stats = FindObjectOfType<PlayerStats>();
        stats.InitPlayerStats();
    }
}
