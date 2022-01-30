using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public List<Image> inventorySlots;
    public Image interactionIcon;
    public Text dialogueText;
    public GameObject dialogueBox;

    private void Awake()
    {
        foreach (Image slot in inventorySlots)
        {
            slot.sprite = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory(ItemInfo item, int itemSlot, bool addItem = true)
    {
        // only can have up to 5 slots
        if (itemSlot < 0 || itemSlot > 5)
        {
            return;
        }

        if (addItem)
        {
            inventorySlots[itemSlot].sprite = item.itemIcon;
        }
        else if (!addItem)
        {
            inventorySlots[itemSlot].sprite = null;
        }
    }

    public void UpdateInteractionIcon()
    {
        // update interaction icon according to the ray trace of the target
    }

    public void UpdateDialogue(string newDialogue = "")
    {
        if (newDialogue == "" || newDialogue == null)
        {
            dialogueText.text = "";
            dialogueBox.SetActive(false);
            return;
        }
        else
        {
            dialogueBox.SetActive(true);
        }

        dialogueText.text = newDialogue;
    }
}
