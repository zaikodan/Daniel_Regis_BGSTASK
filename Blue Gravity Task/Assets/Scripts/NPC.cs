using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactBallon;

    public void Interact()
    {
        Debug.Log("Shop Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactBallon != null)
        {
            interactBallon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactBallon != null)
        {
            interactBallon.SetActive(false);
        }
    }
}
