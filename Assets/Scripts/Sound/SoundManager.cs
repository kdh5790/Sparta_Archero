using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    Lobby,
    Dungeon
}

public enum SFX
{
    UIClick,
    PlayerAttack,
    PlayerOnDamaged,
    PlayerDeath,
    LevelUp,
    EnemyOnDamaged,
    RangeEnemyAttack,
    BossSkill,
    BossDeath,
    StageClear,
    ChestOpen
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip[] bgmClipPrefab;
    [SerializeField] private AudioClip[] sfxClipPrefab;
    [SerializeField] private int sfxSourceCount = 20;
    private List<AudioSource> sfxSources;

    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            PlayMusic(BGM.Lobby);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        bgmSource = GetComponent<AudioSource>();
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < sfxSourceCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            sfxSources.Add(source);
        }
    }

    public void PlaySound(SFX clip)
    {
        AudioSource availableSource = sfxSources.Find(source => !source.isPlaying);
        if (availableSource == null)
        {
            Debug.Log("현재 비어있는 오디오 소스를 찾지 못했습니다.");
            return;
        }
        availableSource.clip = sfxClipPrefab[(int)clip];
        availableSource.Play();
    }

    public void PlayMusic(BGM clip)
    {
        bgmSource.clip = bgmClipPrefab[(int)clip];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SetBGMVolume(float volume)
    {
        float bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        float sfxVolume = Mathf.Clamp01(volume);
        foreach (var source in sfxSources)
        {
            source.volume = sfxVolume;
        }
    }
}
