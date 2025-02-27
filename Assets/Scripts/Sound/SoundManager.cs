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

    // 오디오소스 컴포넌트 추가
    private void InitializeAudioSources()
    {
        bgmSource = GetComponent<AudioSource>();
        sfxSources = new List<AudioSource>();

        // 설정한 숫자만큼 오디오 소스 생성 후 리스트에 넣어주기
        for (int i = 0; i < sfxSourceCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            sfxSources.Add(source);
        }
    }

    // 효과음 재생
    public void PlaySound(SFX clip)
    {
        // 현재 재생중이지 않은 오디오소스 찾기
        AudioSource availableSource = sfxSources.Find(source => !source.isPlaying);

        // 현재 재생 가능한 오디오소스가 없다면 추가로 생성 후 재생
        if (availableSource == null)
        {
            availableSource = gameObject.AddComponent<AudioSource>();
            availableSource.playOnAwake = false;
            sfxSources.Add(availableSource);
        }

        availableSource.clip = sfxClipPrefab[(int)clip];
        availableSource.Play();
    }

    // BGM 재생
    public void PlayMusic(BGM clip)
    {
        bgmSource.clip = bgmClipPrefab[(int)clip];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // BGM 볼륨 조절
    public void SetBGMVolume(float volume)
    {
        float bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    // SFX 볼륨 조절
    public void SetSFXVolume(float volume)
    {
        float sfxVolume = Mathf.Clamp01(volume);
        foreach (var source in sfxSources)
        {
            source.volume = sfxVolume;
        }
    }
}
