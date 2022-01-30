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
    public GameObject interactAimImage;

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

    private NPC interactingNPC;

    private bool playerReply = false;
    public bool buttonPressed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueText.text = "";
    }

    public void PreStartTalking(GameObject NPC)
    {
        interactingNPC = NPC.GetComponent<NPC>();
        outOfRange = false;
        playerReply = false;
        buttonPressed = false;
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
                    StartCoroutine(DisplayString(currentDialogueIndex, dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }
            while (true)
            {
                if(buttonPressed)
                {
                    break;
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

    private IEnumerator DisplayString(int currentDialogueIndex, string stringToDisplay)
    {
        if (!playerReply)
        {
            nameText.text = Names;
        }
        else
        {
            nameText.text = "You";
        }
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;
            bool buttonPopUp;
            if (dialogueLines.Length > 1)
            {
                buttonPopUp = ((questAStart || questBStart) && currentDialogueIndex == dialogueLines.Length - 1);
            }
            else
            {
                buttonPopUp = false;
            }

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if(buttonPopUp)
                    {
                        continue;
                    }
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
            if (buttonPopUp)
            {
                buttonPressed = false;
                ButtonGroup.SetActive(true);
                interactAimImage.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                while (!buttonPressed)
                {
                    yield return 0;
                }
            }
            else
            {
                while (true)
                {
                    if (Input.GetKeyDown(DialogueInput))
                    {
                        break;
                    }
                    yield return 0;
                }
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
            playerReply = true;
        }
    }

    public void DropDialogue()
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

    public void OnButtonYes()
    {
        buttonPressed = true;
        ButtonGroup.SetActive(false);
        interactAimImage.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (questAStart)
        {
            interactingNPC.questATaken = true;
        }
        else if(questBStart)
        {
            interactingNPC.questBTaken = true;
        }
    }
    public void OnButtonNo()
    {
        buttonPressed = true;
        ButtonGroup.SetActive(false);
        interactAimImage.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
