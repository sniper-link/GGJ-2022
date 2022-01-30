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
    public bool questATaken = false;
    private bool questAFinish = false;
    private bool questBSentencesFinish = false;
    public bool questBTaken = false;
    private bool questBFinish = false;

    public bool dialogueActive = false;

    [TextArea(5, 10)]
    public string[] greetingSentences = new string[2];
    [TextArea(5, 10)]
    public string[] questASentences = new string[4];
    [TextArea(5, 10)]
    public string[] questBSentences = new string[4];

    void Awake()
    {
        ChatBackGround = GameObject.Find("DialogueBG").GetComponent<Transform>();
    }

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
	
	void Update () {
        if (dialogueActive)
        {
            Vector3 Pos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
            Pos.y += 175;
            ChatBackGround.position = Pos;
        }
    }

    public void Chat()
    {
        dialogueActive = true;
        dialogueSystem.Names = Name;
        if (!greetingSentencesFinish)
        {
            dialogueSystem.dialogueLines = greetingSentences;
            greetingSentencesFinish = true;
            dialogueSystem.PreStartTalking(this.gameObject);

        }
        else if(!questASentencesFinish || !questAFinish)
        {
            if (questATaken)
            {
                dialogueSystem.dialogueLines = new string[] { questASentences[0] + " (You said yes!)"};
            }
            else
            {
                dialogueSystem.dialogueLines = questASentences;
                dialogueSystem.questAStart = true;
                questASentencesFinish = true;
            }
            dialogueSystem.PreStartTalking(this.gameObject);
        }
        else if (!questBSentencesFinish || !questBFinish)
        {
            if (questBTaken)
            {
                dialogueSystem.dialogueLines = new string[] { questBSentences[0] + " (You said yes!)"};
            }
            else
            {
                dialogueSystem.dialogueLines = questBSentences;
                dialogueSystem.questBStart = true;
                questBSentencesFinish = true;
            }
            dialogueSystem.PreStartTalking(this.gameObject);
        }
        else
        {
            Debug.Log("Finished All Quests!");
        }
    }
}

