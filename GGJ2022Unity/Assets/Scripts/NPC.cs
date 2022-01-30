using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour {

    // public Transform NPCCharacter;
    public Transform ChatBackGround;
    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Awake()
    {
        ChatBackGround = GameObject.Find("DialogueBG").GetComponent<Transform>();
    }

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
	
	void Update () {
          Vector3 Pos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
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

