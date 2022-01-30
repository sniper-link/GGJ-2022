using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSystem: MonoBehaviour {

    public Text nameText;
    public Text dialogueText;

    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;

    public GameObject ButtonGroup;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.F;

    public string Names;

    public string[] dialogueLines;
    public bool questAStart;
    public bool questBStart;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;

    public AudioClip audioClip;
    AudioSource audioSource;

    private GameObject interactingNPC;

    private bool playerReply = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueText.text = "";
    }

    public void preStartTalking(GameObject NPC)
    {
        interactingNPC = NPC;
        outOfRange = false;
        playerReply = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        CameraController.SetOnDialogueTrue();
        PlayerController.SetOnDialogueTrue();
        Interactor.SetOnDialogueTrue();
        if (!dialogueActive)
        {
            dialogueActive = true;
            StartCoroutine(StartDialogue());
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (questAStart || questBStart)
                {
                    ButtonGroup.SetActive(true);
                }
                else if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (!playerReply)
        {
            nameText.text = Names;
            playerReply = true;
        }
        else
        {
            nameText.text = "You";
        }
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {       
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
        OutOfRange();
    }

    public void OutOfRange()
    {
        outOfRange = true;
        letterIsMultiplied = false;
        dialogueActive = false;
        StopAllCoroutines();
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);

        CameraController.SetOnDialogueFalse();
        PlayerController.SetOnDialogueFalse();
        Interactor.SetOnDialogueFalse();
    }

    public void onButtonYes()
    {

    }
    public void onButtonNo()
    {

    }
}
