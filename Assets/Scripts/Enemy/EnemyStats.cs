using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStat", menuName = "Enemy Stats", order = 1)]
public class EnemyStat : ScriptableObject
{
    public string enemyName;
    public int health;
    public int attack;
    public int defense;
    public float speed;
    public int exp;
    //���ݹ��� ��Ÿ��.. ��������� ����, Ư�� �ɷ�.. ���⿡ �߰��ϴ°� ����?
}
