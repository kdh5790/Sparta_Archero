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
    //공격범위 쿨타임.. 드랍아이템 종류, 특수 능력.. 여기에 추가하는게 낫나?
}
