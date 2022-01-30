using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour {

    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
	
	void Update () {
          Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
          Pos.y += 175;
          ChatBackGround.position = Pos;
    }

    public void Chat()
    {
        // this.gameObject.GetComponent<NPC>().enabled = true;
        // dialogueSystem.EnterRangeOfNPC();
        dialogueSystem.Names = Name;
        dialogueSystem.dialogueLines = sentences;
        dialogueSystem.NPCName(this.gameObject);
    }

    public void OutChat()
    {
        dialogueSystem.OutOfRange();
        // this.gameObject.GetComponent<NPC>().enabled = false;
    }
}

