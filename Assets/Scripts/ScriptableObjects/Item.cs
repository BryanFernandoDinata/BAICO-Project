using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject 
{
    [Header("Item Type")]
    public bool isItem;
    public bool isAbility;
    public bool isUpgradeAble;
    public bool requireSomething;

    [Header("Item Details")]
    public string itemName;
    public Sprite itemImage;
    public string description;
    public Item itemRequiered;
    public int itemCost;
    public int itemBuffAmt;
    public bool isOwned;
}
