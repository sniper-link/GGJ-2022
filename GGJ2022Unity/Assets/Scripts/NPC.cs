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

    private bool greetingSentencesFinish = false;
    private bool questASentencesFinish = false;
    private bool questATaken = false;
    private bool questAFinish = false;
    private bool questBSentencesFinish = false;
    private bool questBTaken = false;
    private bool questBFinish = false;

    [TextArea(5, 10)]
    public string[] greetingSentences;
    [TextArea(5, 10)]
    public string[] questASentences;
    [TextArea(5, 10)]
    public string[] questBSentences;

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
        dialogueSystem.Names = Name;
        if (!greetingSentencesFinish)
        {
            dialogueSystem.dialogueLines = greetingSentences;
            greetingSentencesFinish = true;
        }
        else if(!questASentencesFinish || !questAFinish)
        {
            dialogueSystem.dialogueLines = questASentences;
            dialogueSystem.questAStart = true;
            questASentencesFinish = true;
        }
        else if (!questBSentencesFinish || !questBFinish)
        {
            dialogueSystem.dialogueLines = questBSentences;
            dialogueSystem.questBStart = true;
            questBSentencesFinish = true;
        }
        else
        {
            Debug.Log("Finished All Quests!");
        }
        dialogueSystem.preStartTalking(this.gameObject);
    }
}

