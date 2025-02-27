using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬 관리를 위한 네임스페이스
//스테이지의 몬스터가 모두 죽었을때 포탈을 활성화하는 스크립트
public class StageMaanager : MonoBehaviour
{
    public GameObject portal; //포탈 오브젝트 (Inspector에서 연결)
    private bool isPlay; //사운드 재생 확인용

    void Start()
    {
        portal.SetActive(false); //처음에는 포탈 비활성화
    }

    void Update()
    {
        if (AreAllEnemysDead()) //몬스터가 모두 죽었을때
        {
            if (!isPlay) //사운드가 재생중이지 않을때
            {
                isPlay = true; //사운드 재생중으로 변경
                SoundManager.instance.PlaySound(SFX.StageClear); //스테이지 클리어 사운드 재생
            }
            portal.SetActive(true); // 몬스터가 다 죽으면 포탈 활성화
        }
    }

    bool AreAllEnemysDead() //몬스터가 모두 죽었는지 확인하는 함수
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0; //몬스터 태그를 가진 오브젝트의 개수가 0이면 true 반환
    }
}
