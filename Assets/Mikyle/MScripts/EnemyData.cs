using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public int damage;
    public string weakness;
    public string resistance;
}

