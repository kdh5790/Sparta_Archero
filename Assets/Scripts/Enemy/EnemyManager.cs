using UnityEngine;
using System.Collections.Generic;




public class EnemyManager : MonoBehaviour
{
    public List<EnemyAI> enemies = new List<EnemyAI>();

    public void RegisterEnemy(EnemyAI enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
            Debug.Log("��ϵ� ��: " + enemy.name);
        }
    }

    public void UnregisterEnemy(EnemyAI enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log("���ŵ� ��: " + enemy.name);
        }
    }
}
