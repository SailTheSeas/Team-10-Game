using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterData playerData;
    // Start is called before the first frame update

    public string characterName;
    public int playerHealth;
    public int playerMaxHP;
    public int playerMP;
    public int playerMaxMP;
    public int playerPhysAttack;
    public int playerBulletAmount;
    public int PGuard;
    public int playerBulletDamage;
    public List<PersonaMove> playerMoves;
    public bool isGuarding = false;
    public string playerName;
    //[SerializeField] List<Item> items;

    public string CharacterName { get => characterName; set => characterName = value; }
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public int PlayerMP { get => playerMP; set => playerMP = value; }
    public int PlayerPhysAttack { get => playerPhysAttack; set => playerPhysAttack = value; }
    public int PlayerBulletAmount { get => playerBulletAmount; set => playerBulletAmount = value; }
    public int PlayerBulletDamage { get => playerBulletDamage; set => playerBulletDamage = value; }
    public int PlayerGuard { get => PGuard; set => PGuard = value; }
    public List<PersonaMove> PlayerMoves { get => playerMoves; set => playerMoves = value; }
   
    //public List<Item> Items { get => items; set => items = value; }

    [Header("Animation")]
    public Animator playerAnim;

    [Header("Particles")]
    public List<GameObject> particleSystems;



    void Start()
    {
        CharacterName = playerData.characterName;
        PlayerHealth = playerData.health;
        playerMaxHP = PlayerHealth;
        PlayerMP = playerData.mp;
        playerMaxMP = PlayerMP;
        PlayerPhysAttack = playerData.physicalAttack;
        PlayerBulletAmount = playerData.bulletAmount;
        PlayerBulletDamage = playerData.bulletDamage;
        PlayerMoves = playerData.personaMoves;
        PlayerGuard = playerData.Guard;
        //Items = playerData.items;

        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }



}
