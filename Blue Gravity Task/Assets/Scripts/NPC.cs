using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactBalloon;
    [SerializeField] ShopManager shop;
    [SerializeField] Item[] itemsForSale;

    public Item[] ItemsForSale { get => itemsForSale; }

    public void Interact()
    {
        shop.Npc = this;
        shop.OpenShop();
        Debug.Log("Shop Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactBalloon != null)
        {
            interactBalloon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactBalloon != null)
        {
            interactBalloon.SetActive(false);
        }
    }
}
