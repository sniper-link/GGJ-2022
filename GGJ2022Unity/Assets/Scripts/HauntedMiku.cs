using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedMiku : MonoBehaviour
{
    public Transform hauntedMiku;
    public Animator mikuController;
    public List<Transform> moveLocs;
    public int locIndex = 0;

    public void MoveMiku()
    {
        if (locIndex < moveLocs.Count)
        {
            if (locIndex == 1)
            {
                mikuController.SetTrigger("Idle");
                mikuController.SetTrigger("Crawl");
            }
                
            if (locIndex == 4)
            {
                mikuController.SetTrigger("Idle");
                mikuController.SetTrigger("SpinAttack");
            }

            hauntedMiku.position = moveLocs[locIndex].position;
            locIndex++;
        }    
        else
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveMiku();
        }
    }

}
