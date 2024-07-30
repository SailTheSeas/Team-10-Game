using System;

[Serializable]
public class Item
{
    public string itemName;
    public int amount;
    //public bool isSingleTarget;
    public bool isHP;
    public bool isUsed = false;

    public Item(string itemName, int amount, bool isHP, bool isUsed)
    {
        this.itemName = itemName;
        this.amount = amount;
        this.isHP = isHP;
        this.isUsed = isUsed;
    }
}
