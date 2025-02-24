using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill
{
    DamageBoost = 1,        //O  1.���� �ν�Ʈ : ���ݷ��� 30% �����Ѵ�.
    AttackSpeedBoost,       //O  2.���� �ӵ� �ν�Ʈ : ���� �ӵ��� 25% �����Ѵ�.
    CriticalMaster,         //O  3.ũ��Ƽ�� ������ : ũ��Ƽ�� ������ 40%, ũ��Ƽ�� Ȯ�� 10%�� �����Ѵ�.
    HealthBoost,            //O  4.HP�ν�Ʈ : �ִ� ü�� 20%�� ������Ű�� ������ ��ŭ ü�� ȸ��, �����ϴ� ü���� �⺻ �ִ� ü��(���� 600) ����
    Rage,                   //O  5.�г� : ���� ü�� 1% �� �������� 1.2% �����Ѵ�.
    Invincibility,          //O  6.���� : ĳ���͸� 10�ʸ��� 2�� �� �������·� �����.
    Rebound,                //O  7.�ݵ� : ������ ���� ȭ���� ��ó�� ������ �ִ� 2ȸ ƨ�� ������ ���� �����Ѵ�. ƨ�� �� ���� �������� 30%�� ���ҵȴ�.
    PiercingShot,           //O  8.���뼦 : ȭ���� ���� �����ϸ� ���� ���� ȭ���� ���ط��� 67%�� �����Ѵ�.
    MultiShot,              //O  9.��Ƽ�� : ��� ������ �ѹ� �� �߻������� ���ݼӵ� 15% �϶�, �������ݷ� 10% �϶�
    FrontArrowPlus,         //O 10.���� ȭ�� +1 : �������� �߻��ϴ� ȭ�� �߰�
    DiagonalArrowPlus,      //O 11.�缱 ȭ�� +1 : �밢������ ������ ȭ�� �߰�, 2ȸ ������ 30��, 60���� ȭ���� ������.
    SideArrowPlus,          //O 12.���� ȭ�� +1 : �¿� 90���� �߻��ϴ� ȭ�� �߰�
    BackArrowPlus,          //O 13.�Ĺ� ȭ�� +1 : �� �ڷ� ȭ���� �߰�
    HeadShot,               //X 14.��弦 : ����� ���� �� 12.5%�� Ȯ���� ���� ����Ų��. ��弦�� �ߵ��� ��ȸ�� �ѹ����̹Ƿ�, ó�� ������ ȭ���� ��簡 �ƴ϶�� ��弦 �ߵ�X
    DodgeMastery            //O 15.ȸ�� ������ : ȸ�� Ȯ���� 20% �þ��.
}

[CreateAssetMenu(fileName = "SkillInfo", menuName = "Scriptable Object/SkillInfo", order = int.MaxValue)]
public class SkillInfo : ScriptableObject
{
    public Skill SkillID;
    public string SkillName;
    public string SkillDescription;
    public bool IsReacquirable;
}
