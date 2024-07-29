public class Item
{
    public string itemName;
    public int healAmount;
    public bool isSingleTarget;

    public Item(string itemName, int healAmount, bool isSingleTarget)
    {
        this.itemName = itemName;
        this.healAmount = healAmount;
        this.isSingleTarget = isSingleTarget;
    }
}
