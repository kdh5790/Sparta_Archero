using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill
{
    DamageBoost = 1,        //O  1.공격 부스트 : 공격력이 30% 증가한다.
    AttackSpeedBoost,       //O  2.공격 속도 부스트 : 공격 속도가 25% 증가한다.
    CriticalMaster,         //O  3.크리티컬 마스터 : 크리티컬 데미지 40%, 크리티컬 확률 10%가 증가한다.
    HealthBoost,            //O  4.HP부스트 : 최대 체력 20%를 증가시키며 증가한 만큼 체력 회복, 증가하는 체력은 기본 최대 체력(현재 600) 기준
    Rage,                   //O  5.분노 : 잃은 체력 1% 당 데미지가 1.2% 증가한다.
    Invincibility,          //O  6.무적 : 캐릭터를 10초마다 2초 간 무적상태로 만든다.
    Rebound,                //O  7.반동 : 적에게 맞은 화살이 근처의 적에게 최대 2회 튕겨 나가며 적을 공격한다. 튕길 때 마다 데미지가 30%씩 감소된다.
    PiercingShot,           //O  8.관통샷 : 화살이 적을 관통하며 관통 후의 화살은 피해량이 67%로 감소한다.
    MultiShot,              //O  9.멀티샷 : 모든 공격을 한번 더 발사하지만 공격속도 15% 하락, 최종공격력 10% 하락
    FrontArrowPlus,         //O 10.전방 화살 +1 : 전방으로 발사하는 화살 추가
    DiagonalArrowPlus,      //O 11.사선 화살 +1 : 대각선으로 나가는 화살 추가, 2회 얻으면 30도, 60도로 화살이 나간다.
    SideArrowPlus,          //O 12.측면 화살 +1 : 좌우 90도로 발사하는 화살 추가
    BackArrowPlus,          //O 13.후방 화살 +1 : 등 뒤로 화살을 추가
    HeadShot,               //X 14.헤드샷 : 잡몹을 공격 시 12.5%의 확률로 적을 즉사시킨다. 헤드샷이 발동할 기회는 한번뿐이므로, 처음 명중한 화살이 즉사가 아니라면 헤드샷 발동X
    DodgeMastery            //O 15.회피 마스터 : 회피 확률이 20% 늘어난다.
}

[CreateAssetMenu(fileName = "SkillInfo", menuName = "Scriptable Object/SkillInfo", order = int.MaxValue)]
public class SkillInfo : ScriptableObject
{
    public Skill SkillID;
    public string SkillName;
    public string SkillDescription;
    public bool IsReacquirable;
}
