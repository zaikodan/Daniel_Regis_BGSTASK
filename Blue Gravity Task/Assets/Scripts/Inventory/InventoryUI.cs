using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    [SerializeField]
    Transform storageContent;
    [SerializeField]
    GameObject inventoryWindow;


    [Header("Texts")]

    [SerializeField]
    Text itemName;
    [SerializeField]
    Text itemDescription;
    [SerializeField]
    Text itemCost;
    [SerializeField]
    Text playerMoney;

    [Header("Icon")]

    [SerializeField]
    Image itemIcon;

    [Header("Previews")]

    [SerializeField] 
    Image[] equipedHoodPreview;
    [SerializeField]
    Image[] equipedArmorPreview;
    [SerializeField]
    Image[] equipedBootPreview;

    [Header("Buttons")]

    [SerializeField]
    Button equipButton;
    [SerializeField]
    Button exitButton;

    [Header("Prefab")]

    [SerializeField]
    GameObject ButtonPrefab;



    private void Awake()
    {
        inventory = GetComponent<Inventory>();

        equipButton.onClick.AddListener(inventory.EquipItem);
        exitButton.onClick.AddListener(inventory.CloseInventory);
        
    }

    internal void UpdateMoney(int value)
    {
        playerMoney.text = "Money: " + value;
    }

    internal void SetWindow(bool active)
    {
        inventoryWindow.SetActive(active);
        UpdateInformations();
    }

    internal void AddButton(Item item)
    {
        GameObject buttonInstantiated = Instantiate(ButtonPrefab);
        buttonInstantiated.transform.SetParent(storageContent, false);
        buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => inventory.SelectItem(item));
        buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => UpdateInformations(item));
        buttonInstantiated.transform.GetChild(0).GetComponent<Image>().sprite = item.ItemIcon;
    }

    internal void RemoveButton(int index)
    {
        Destroy(storageContent.GetChild(index).gameObject);
    }

    internal void UpdateInformations()
    {
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        itemCost.text = string.Empty;
        itemIcon.enabled = false;
        equipButton.interactable = false;
    }

    private void UpdateInformations(Item item)
    {
        itemName.text = item.ItemName;
        itemDescription.text = item.ItemDescription;
        itemCost.text = item.Price.ToString();
        itemIcon.sprite = item.ItemIcon;
        itemIcon.enabled = true;
        equipButton.interactable = item.Equipable;

        Text equipButtonText = equipButton.GetComponentInChildren<Text>();
        if (item != inventory.HoodEquiped && item != inventory.ArmorEquiped && item != inventory.BootEquiped)
        {
            equipButtonText.text = "Equip";
        }
        else
        {
            equipButton.interactable = false;
            equipButtonText.text = "Equiped";
        }
    }

    internal void UpdatePreview(Item.TypesOfItem itemType)
    {
        switch (itemType)
        {
            case Item.TypesOfItem.Hood:

                for (int i = 0; i < equipedHoodPreview.Length; i++)
                {
                    equipedHoodPreview[i].sprite = inventory.HoodEquiped.Sprites[i];
                }

                break;
            case Item.TypesOfItem.Armor:

                for (int i = 0; i < equipedArmorPreview.Length; i++)
                {
                    equipedArmorPreview[i].sprite = inventory.ArmorEquiped.Sprites[i];
                }

                break;
            case Item.TypesOfItem.Boots:

                for (int i = 0; i < equipedBootPreview.Length; i++)
                {
                    equipedBootPreview[i].sprite = inventory.BootEquiped.Sprites[i];
                }

                break;
        }
        
    }
}
