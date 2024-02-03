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

    Item itemSelected;
    GameObject buttonSelected;
    internal bool buying;

    public NPC Npc { get => npc; set => npc = value; }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        playerController = FindObjectOfType<PlayerController>();
        shopUI = GetComponent<ShopUI>();
    }

    private void Start()
    {
        shopUI.SetupStoreToSell(inventory);
        shopUI.UpdateMoney(inventory.Money);
    }

    private void SetupShop()
    {
        shopUI.SetupStoreToBuy(npc);
        buying = true;
        shopUI.ResetPreview();
        shopUI.SwitchWindows(buying);
        shopUI.SetShopWindow(buying);
    }

    internal void SelectItemToBuy(int itemIndex)
    {
        itemSelected = (npc.ItemsForSale[itemIndex]);
        shopUI.SetPreview(itemSelected);
        shopUI.SetBuyButton(inventory.Money >= itemSelected.Price);
    }
    internal void SelectItemToSell(Item itemSelect, GameObject buttonClicked)
    {
        itemSelected = itemSelect;
        buttonSelected = buttonClicked;

        shopUI.SetSellButton(!itemSelect.Equiped);
        
    }

    internal void BuyItem()
    {
        Item itemPurchased = ScriptableObject.CreateInstance<Item>();
        itemPurchased.SetItem(itemSelected);
        inventory.Buy(itemPurchased);

        shopUI.UpdateMoney(inventory.Money);
        shopUI.SetBuyButton(inventory.Money >= itemPurchased.Price);

        shopUI.AddItemToSell(itemPurchased);
    }

    internal void SellItem()
    {
        inventory.Sell(itemSelected);

        shopUI.UpdateMoney(inventory.Money);
        shopUI.SetSellButton(false);
        Destroy(buttonSelected);
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
