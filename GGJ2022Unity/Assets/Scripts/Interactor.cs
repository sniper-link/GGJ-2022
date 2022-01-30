using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayermask = 8;
    Interactable interactable;
    public Image interactAimImage;
    public Sprite defaultIcon;
    public Vector2 defaultIconSize;
    public Sprite defaultInteractIcon;
    public Vector2 defaultInteractIconSize;

    static private bool onDialogue = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                if (interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                }
                if(interactable.interactIcon != null)
                {
                    interactAimImage.sprite = interactable.interactIcon;
                    if(interactable.iconSize == Vector2.zero)
                    {
                        interactAimImage.rectTransform.sizeDelta = defaultInteractIconSize;
                    }
                    else
                    {
                        interactAimImage.rectTransform.sizeDelta = interactable.iconSize;
                    }
                }
                else
                {
                    interactAimImage.sprite = defaultInteractIcon;
                    interactAimImage.rectTransform.sizeDelta = defaultInteractIconSize;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(!onDialogue)
                    {
                        interactable.onInteract.Invoke();
                    }
                }
            }
        }
        else
        {
            if(interactAimImage.sprite != defaultIcon)
            {
                interactAimImage.sprite = defaultIcon;
                interactAimImage.rectTransform.sizeDelta = defaultIconSize;
            }
        }
    }
    static public void SetOnDialogueTrue()
    {
        onDialogue = true;
    }
    static public void SetOnDialogueFalse()
    {
        onDialogue = false;
    }
}