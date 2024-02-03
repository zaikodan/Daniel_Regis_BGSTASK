using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryUI))]
public class Inventory : MonoBehaviour
{
    private List<Item> itemsList = new List<Item>();

    PlayerController playerController;
    PlayerRender playerRender;
    InventoryUI inventoryUI;

    Item hoodEquiped, armorEquiped, bootEquiped;
    Item itemSelected;

    [SerializeField]
    Item[] initialItems;

    const int initialMoney = 100;
    int money;

    public List<Item> ItemsList { get => itemsList; }

    public int Money { get => money; }
    public Item HoodEquiped { get => hoodEquiped; }
    public Item ArmorEquiped { get => armorEquiped; }
    public Item BootEquiped { get => bootEquiped; }

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerRender = playerController.GetComponent<PlayerRender>();
        inventoryUI = GetComponent<InventoryUI>();

       
    }

    private void Start()
    {
        EquipInitials();

        money = initialMoney;
        inventoryUI.UpdateMoney(money);
    }

    private void EquipInitials()
    {
        foreach (Item item in initialItems)
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            newItem.SetItem(item);
            AddItem(newItem);
            EquipItem(newItem);
        }
    }

    internal void SelectItem(Item item)
    {
        itemSelected = item;
    }

    private void EquipItem(Item item)
    {
        ChangeEquipment(item);
        inventoryUI.UpdatePreview(item.ItemType);
    }

    internal void EquipItem()
    {
        ChangeEquipment(itemSelected);
        inventoryUI.UpdatePreview(itemSelected.ItemType);
        itemSelected = null;
        inventoryUI.UpdateInformations();
        
    }

    internal void ChangeEquipment(Item item)
    {
        switch (item.ItemType)
        {
            case Item.TypesOfItem.Hood:

                if (hoodEquiped != null)
                {
                    hoodEquiped.Equiped = false;
                }
                hoodEquiped = item;
                hoodEquiped.Equiped = true;

                break;
            case Item.TypesOfItem.Armor:

                if (armorEquiped != null)
                {
                    armorEquiped.Equiped = false;
                }
                armorEquiped = item;
                armorEquiped.Equiped = true;

                break;
            case Item.TypesOfItem.Boots:

                if (bootEquiped != null)
                {
                    bootEquiped.Equiped = false;
                }
                bootEquiped = item;
                bootEquiped.Equiped = true;

                break;
        }

        playerRender.SetEquipment(item);
    }

    private void AddItem(Item newItem)
    {
        itemsList.Add(newItem);
        inventoryUI.AddButton(newItem);
    }

    private void RemoveItem(Item newItem)
    {
        inventoryUI.RemoveButton(itemsList.IndexOf(newItem));
        itemsList.Remove(newItem);
    }

    public void Buy(Item itemPurchased)
    {
        money -= itemPurchased.Price;
        AddItem(itemPurchased);
        inventoryUI.UpdateMoney(money);
    }

    public void Sell(Item itemSold)
    {
        money += itemSold.Price;
        RemoveItem(itemSold);
        inventoryUI.UpdateMoney(money);
    }

    public void OpenInventory()
    {
        inventoryUI.SetWindow(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetWindow(false);
        playerController.EndInteraction();
    }
}
