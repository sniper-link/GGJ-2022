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
    // Start is called before the first frame update
    void Start()
    {
        if(interactAimImage == null)
        {
            try
            {
                interactAimImage = GameObject.Find("Interaction_Aim_Image").GetComponent<Image>();
            }
            catch(System.Exception e)
            {
                Debug.Log($"----- You forgot to create a Interaction_Aim_Image for PlayerUI! ----- Exception: {e}");
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 2))
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
                    interactable.onInteract.Invoke();
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
}