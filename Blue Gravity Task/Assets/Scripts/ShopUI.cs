using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Button buyButton, sellButton, npcButton, inventoryButton, exitButton;
    [SerializeField] GameObject buyWindow, sellWindow, shopWindow, buyContent, sellContent;
    [SerializeField] GameObject shopButtonPrefab;
    [SerializeField] Text itemName, itemDescription, itemCost, playerMoney;
    [SerializeField] Image itemIcon;

    ShopManager shopManager;

    private void Awake()
    {
        shopManager = GetComponent<ShopManager>();

        InitializeButtons();
    }

    internal void SetupStoreToBuy(NPC npc)
    {
        int greaterLength = Mathf.Max(npc.ItemsForSale.Length, buyContent.transform.childCount);
        for (int i = 0; i < greaterLength; i++)
        {
            GameObject currentButton;
            if (i >= npc.ItemsForSale.Length)
            {
                Destroy(buyContent.transform.GetChild(i).gameObject);
                continue;
            }
            else if(i >= buyContent.transform.childCount)
            {
                currentButton = Instantiate(shopButtonPrefab);
                currentButton.transform.SetParent(buyContent.transform, false);
            }
            else
            {
                currentButton = buyContent.transform.GetChild(i).gameObject;
            }

            int index = i;

            SetupButton(currentButton.GetComponent<Button>(), () => shopManager.SelectItemToBuy(index), npc.ItemsForSale[i].ItemIcon);
        }
    }

    internal void SetupStoreToSell(Inventory inventory)
    {
        foreach(Item item in inventory.ItemsList)
        {
            AddItemToSell(item);
        }
    }

    internal void AddItemToSell(Item item)
    {
        GameObject buttonInstantiated = Instantiate(shopButtonPrefab);
        buttonInstantiated.transform.SetParent(sellContent.transform, false);

        SetupButton(buttonInstantiated.GetComponent<Button>(), () => shopManager.SelectItemToSell(item, buttonInstantiated), item.ItemIcon);
    }

    private void SetupButton(Button targetButton, UnityAction call, Sprite icon)
    {
        targetButton.onClick.AddListener(call);
        targetButton.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    private void InitializeButtons()
    {
        Debug.Log("Buttons Initialized");
        buyButton.onClick.AddListener(shopManager.BuyItem);
        sellButton.onClick.AddListener(shopManager.SellItem);
        npcButton.onClick.AddListener(() => SwitchWindows(true));
        inventoryButton.onClick.AddListener(() => SwitchWindows(false));
        exitButton.onClick.AddListener(shopManager.CloseShop);
    }

    internal void SwitchWindows(bool buying)
    {
        shopManager.buying = buying;
        buyWindow.SetActive(buying);
        sellWindow.SetActive(!buying);

        SetBuyButton(false);
        SetSellButton(false);
        ResetPreview();
    }

    internal void ResetPreview()
    {
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        itemCost.text = string.Empty;
        itemIcon.enabled = false;
    }

    internal void SetPreview(Item item)
    {
        itemName.text = item.ItemName;
        itemDescription.text = item.ItemDescription;
        itemCost.text = "Cost: " + item.Price;
        itemIcon.sprite = item.ItemIcon;
        itemIcon.enabled = true;

    }

    internal void UpdateMoney(int value)
    {
        playerMoney.text = "Money: " + value;
    }

    internal void SetBuyButton(bool active)
    {
        buyButton.interactable = active;
    }

    internal void SetSellButton(bool active)
    {
        sellButton.interactable = active;
    }

    internal void SetShopWindow(bool active)
    {
        shopWindow.SetActive(active);
    }
}
