using System;

[Serializable]
public class EnemyMove
{
    public string moveName;
    public int damageValue;
    public string elementalType;

    public EnemyMove(string moveName, int damageValue, string elementalType)
    {
        this.moveName = moveName;
        this.damageValue = damageValue;
        this.elementalType = elementalType;
    }
}
