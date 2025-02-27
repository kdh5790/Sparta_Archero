using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStat", menuName = "Enemy Stats", order = 1)]
public class EnemyStat : ScriptableObject   //��ũ���ͺ� ������Ʈ�� ���� ���� ����
{
    public string enemyName;
    public int health;
    public int attack;
    public int defense;
    public float speed;
    public int exp;
}
