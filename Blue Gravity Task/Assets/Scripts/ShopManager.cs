using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShopUI))]
public class ShopManager : MonoBehaviour
{
    NPC npc;
    Inventory inventory;
    ShopUI shopUI;

    int itemSelected;

    public NPC Npc { get => npc; set => npc = value; }
    public Inventory Inventory { get => inventory; }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        shopUI = GetComponent<ShopUI>();
    }

    public void SetupShop()
    {
        shopUI.GenerateButtons();

    }

    internal void SelectItem(int itemIndex)
    {
        itemSelected = itemIndex;
    }

    internal void BuyItem()
    {
        inventory.Money -= npc.ItemsForSale[itemSelected].Price;
        inventory.ItemsList.Add(npc.ItemsForSale[itemSelected]);
    }

    internal void SellItem()
    {
        Item itemSelling = npc.ItemsForSale[itemSelected];

        if (itemSelling.Equipable)
        {
            inventory.Money += inventory.ItemsList[itemSelected].Price / 2;
        }
        else
        {
            inventory.Money += inventory.ItemsList[itemSelected].Price;
        }

        inventory.ItemsList.Remove(inventory.ItemsList[itemSelected]);

    }
}
