using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerStats stats;
    public Weapon_Bow bow;
    public ArrowManager arrowManager;

    public bool isDead = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        arrowManager = FindObjectOfType<ArrowManager>();
        bow = FindObjectOfType<Weapon_Bow>();
        stats = FindObjectOfType<PlayerStats>();
        stats.InitPlayerStats();
    }
}