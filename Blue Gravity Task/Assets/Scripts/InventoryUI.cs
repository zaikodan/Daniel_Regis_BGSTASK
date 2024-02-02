using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Item;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject content, inventoryWindow;
    [SerializeField]
    GameObject ButtonPrefab;
    [SerializeField]
    Text itemName, itemDescription, itemCost;
    [SerializeField]
    Image itemIcon;
    [SerializeField]
    Image[] equipedHeadPreview, equipedTorsoPreview, equipedLegPreview;
    [SerializeField]
    Button equipButton, exitButton;

    Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();

        equipButton.onClick.AddListener(inventory.EquipItem);
        exitButton.onClick.AddListener(inventory.CloseInventory);
        
    }

    internal void SetWindow(bool active)
    {
        inventoryWindow.SetActive(active);
        UpdateInformations();
    }

    internal void AddButton(Item item)
    {
        GameObject buttonInstantiated = Instantiate(ButtonPrefab);
        buttonInstantiated.transform.SetParent(content.transform, false);
        buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => inventory.SelectItem(item));
        buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => UpdateInformations(item));
    }

    internal void RemoveButton(int index)
    {
        Destroy(content.transform.GetChild(index).gameObject);
    }

    internal void UpdateInformations()
    {
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        itemCost.text = string.Empty;
        itemIcon.sprite = null;
        equipButton.interactable = false;
    }

    private void UpdateInformations(Item item)
    {
        itemName.text = item.ItemName;
        itemDescription.text = item.ItemDescription;
        itemCost.text = item.Price.ToString();
        itemIcon.sprite = item.ItemIcon;
        equipButton.interactable = item.Equipable;

        Text equipButtonText = equipButton.GetComponentInChildren<Text>();
        if (item != inventory.HeadEquiped && item != inventory.TorsoEquiped && item != inventory.LegEquiped)
        {
            equipButtonText.text = "Equip";
        }
        else
        {
            equipButton.interactable = false;
            equipButtonText.text = "Equiped";
        }
    }

    internal void UpdatePreview(TypesOfItems itemType)
    {
        switch (itemType)
        {
            case TypesOfItems.Head:

                for (int i = 0; i < equipedHeadPreview.Length; i++)
                {
                    equipedHeadPreview[i].sprite = inventory.HeadEquiped.Sprites[i];
                }

                break;
            case TypesOfItems.Torso:

                for (int i = 0; i < equipedTorsoPreview.Length; i++)
                {
                    equipedTorsoPreview[i].sprite = inventory.TorsoEquiped.Sprites[i];
                }

                break;
            case TypesOfItems.Legs:

                for (int i = 0; i < equipedLegPreview.Length; i++)
                {
                    equipedLegPreview[i].sprite = inventory.LegEquiped.Sprites[i];
                }

                break;
        }
        
    }
}
