using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    public EnemyData enemyData;

    public string enemyName;
    public int enemyHealth;
    public int enemyDamage;
    public string enemyWeakness;
    public string enemyResistance;

    public string EnemyName { get => enemyName; set => enemyName = value; }
    public int EnemyHealth { get => enemyHealth; set => enemyHealth = value; }
    public int EnemyDamage { get => enemyDamage; set => enemyDamage = value; }
    public string EnemyWeakness { get => enemyWeakness; set => enemyWeakness = value; }
    public string EnemyResistance { get => enemyResistance; set => enemyResistance = value; }

    //What are we communicating with the state machine?
    //Communicate the Player Affordance (decisions they can make/actions they can take) from discrete states

    void Start()
    {
        EnemyName = enemyData.enemyName;
        EnemyHealth = enemyData.health;
        EnemyDamage = enemyData.damage;
        EnemyWeakness = enemyData.weakness;
        EnemyResistance = enemyData.resistance;
    }

    // Update is called once per frame
    void Update()
    {

    }




}
