using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 플레이어가 장애물에 충돌했을때 데미지를 주는 스크립트(Stage_11 ~ Stage_19)
public class CollosionA : MonoBehaviour
{
    public int damage = 20; //장애물이 주는 데미지
    public float damageInterval = 1.0f; //데미지를 주는 간격 (초)
    private bool isDamaging = false; //데미지 코루틴 실행 여부

    private void OnTriggerEnter2D(Collider2D collision) //플레이어가 장애물에 충돌했을때 호출되는 함수
    {
        if (collision.CompareTag("Player") && !isDamaging) //플레이어와 충돌하고 데미지 코루틴이 실행중이지 않을때
        {
            StartCoroutine(DealDamageOverTime(collision.GetComponent<PlayerStats>())); // 데미지 코루틴 실행
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //플레이어가 장애물에서 벗어났을때 호출되는 함수
    {
        if (collision.CompareTag("Player"))
        {
            isDamaging = false; //충돌에서 벗어나면 데미지 중지
        }
    }

    private IEnumerator DealDamageOverTime(PlayerStats player) //데미지를 주는 코루틴
    {
        isDamaging = true; //데미지 코루틴 실행중

        while (isDamaging && player != null) //데미지 코루틴이 실행중이고 플레이어가 존재할때
        {
            player.OnDamaged(damage); //플레이어에게 데미지를 줌
            yield return new WaitForSeconds(damageInterval); //데미지 간격만큼 대기
        }

    }
}