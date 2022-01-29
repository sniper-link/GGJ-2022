using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    const int inventorySize = 5;

    public List<ItemInfo> itemList;
    public PlayerUI playerUI;

    public void AddItem(ItemInfo newItem)
    {
        if (!itemList.Contains(newItem) && itemList.Count < inventorySize)
        {
            itemList.Add(newItem);
        }
    }

    public bool HaveItem(ItemInfo item)
    {
        return itemList.Contains(item);
    }

    public void UseItem(ItemInfo item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
        }
    }
}
