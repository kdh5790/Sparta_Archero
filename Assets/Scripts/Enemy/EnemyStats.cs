using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStat", menuName = "Enemy Stats", order = 1)]
public class EnemyStat : ScriptableObject   //스크립터블 오브젝트로 몬스터 정보 정리
{
    public string enemyName;
    public int health;
    public int attack;
    public int defense;
    public float speed;
    public int exp;
}
