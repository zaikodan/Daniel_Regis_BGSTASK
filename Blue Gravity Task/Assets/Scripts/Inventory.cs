using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Item> itemsList = new List<Item>();

    PlayerRender playerRender;

    Item headEquiped, torsoEquiped, legEquiped;
    Item itemSelected;
    [SerializeField]
    Item[] initialItems;

    InventoryUI inventoryUI;

    int money = 100;

    public List<Item> ItemsList { get => itemsList; }

    public int Money { get => money; }
    public Item HeadEquiped { get => headEquiped; }
    public Item TorsoEquiped { get => torsoEquiped; }
    public Item LegEquiped { get => legEquiped; }

    private void Awake()
    {
        playerRender = GetComponent<PlayerRender>();
        inventoryUI = FindObjectOfType<InventoryUI>();

        EquipInitials();
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
            case Item.TypesOfItems.Head:

                if (headEquiped != null)
                {
                    headEquiped.Equiped = false;
                }
                headEquiped = item;
                headEquiped.Equiped = true;

                break;
            case Item.TypesOfItems.Torso:

                if (torsoEquiped != null)
                {
                    torsoEquiped.Equiped = false;
                }
                torsoEquiped = item;
                torsoEquiped.Equiped = true;

                break;
            case Item.TypesOfItems.Legs:

                if (legEquiped != null)
                {
                    legEquiped.Equiped = false;
                }
                legEquiped = item;
                legEquiped.Equiped = true;

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
    }
}
