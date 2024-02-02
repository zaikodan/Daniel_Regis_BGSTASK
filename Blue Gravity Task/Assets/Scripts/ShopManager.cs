using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShopUI))]
public class ShopManager : MonoBehaviour
{
    NPC npc;
    Inventory inventory;
    PlayerController playerController;
    ShopUI shopUI;

    int itemSelected;
    GameObject buttonSelected;
    internal bool buying;

    public NPC Npc { get => npc; set => npc = value; }
    public Inventory Inventory { get => inventory; }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        playerController = FindObjectOfType<PlayerController>();
        shopUI = GetComponent<ShopUI>();
    }

    public void SetupShop()
    {
        buying = true;
        shopUI.GenerateButtons();
        shopUI.SetPreview();
        shopUI.SetShopWindow(buying);
    }

    internal void SelectItem(int itemIndex, GameObject buttonClicked)
    {
        itemSelected = itemIndex;
        buttonSelected = buttonClicked;

        if (buying)
        {
            shopUI.SetPreview(npc.ItemsForSale[itemSelected]);
            shopUI.SetBuyButton(inventory.Money >= npc.ItemsForSale[itemSelected].Price);
        }
        else
        {
            shopUI.SetSellButton(true);
        }
    }

    internal void BuyItem()
    {
        inventory.Money -= npc.ItemsForSale[itemSelected].Price;
        inventory.ItemsList.Add(npc.ItemsForSale[itemSelected]);

        shopUI.SetBuyButton(inventory.Money >= npc.ItemsForSale[itemSelected].Price);

        shopUI.GenerateButtons();
    }

    internal void SellItem()
    {
        inventory.Money += inventory.ItemsList[itemSelected].Price;

        inventory.ItemsList.Remove(inventory.ItemsList[itemSelected]);

        shopUI.SetSellButton(false);
        DestroyImmediate(buttonSelected);
        shopUI.UpdateButtons();
    }

    public void OpenShop()
    {
        SetupShop();
        
    }

    internal void CloseShop()
    {
        playerController.EndInteraction();
        shopUI.SetShopWindow(false);
    }
}
