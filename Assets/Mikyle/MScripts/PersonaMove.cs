using System;

[Serializable]
public class PersonaMove
{
    public string moveName;
    public int damageValue;
    public string elementalType;
    public int mpCost;
    //public bool isSingleTarget;

    public PersonaMove(string moveName, int damageValue, string elementalType, int mpCost/*, bool isSingleTarget*/)
    {
        this.moveName = moveName;
        this.damageValue = damageValue;
        this.elementalType = elementalType;
        this.mpCost = mpCost;
        //this.isSingleTarget = isSingleTarget;
    }
}

