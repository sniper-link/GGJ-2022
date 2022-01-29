using UnityEngine;

[CreateAssetMenu(fileName = "NewItemInfo", menuName = "Items/Create New Item Info", order = 2)]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}
