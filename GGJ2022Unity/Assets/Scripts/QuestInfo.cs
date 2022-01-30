using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestInfo", menuName = "Quests/Create New Quest Info", order = 2)]
public class QuestInfo : ScriptableObject
{
    public int questID;
    public List<ItemInfo> requiredItem;
    public bool questCompleted;
}
