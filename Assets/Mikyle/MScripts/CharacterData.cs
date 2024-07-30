using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int health;
    public int mp;
    public int physicalAttack;
    public int bulletAmount;
    public int bulletDamage;
    public List<PersonaMove> personaMoves;
    //public List<Item> items;
}

