using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerStats stats;
    public ArrowManager arrowManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        arrowManager = FindObjectOfType<ArrowManager>();
        stats = FindObjectOfType<PlayerStats>();
        stats.InitPlayerStats();
    }
}
