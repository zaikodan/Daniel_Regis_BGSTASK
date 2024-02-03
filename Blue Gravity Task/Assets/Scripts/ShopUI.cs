using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Button buyButton, sellButton, npcButton, inventoryButton, exitButton;
    [SerializeField] GameObject buyWindow, sellWindow, shopWindow, buyContent, sellContent, previewLegsParent, previewTopParent, previewHeadParent;
    [SerializeField] GameObject shopButtonPrefab;
    [SerializeField] Text itemName, itemDescription, itemCost;

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

            currentButton.GetComponent<Button>().onClick.AddListener(() => shopManager.SelectItemToBuy(index));
            currentButton.GetComponentInChildren<Text>().text = npc.ItemsForSale[i].ItemName;
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
        buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => shopManager.SelectItemToSell(item, buttonInstantiated));
        buttonInstantiated.GetComponentInChildren<Text>().text = item.ItemName;
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
        previewHeadParent.SetActive(false);
        previewTopParent.SetActive(false);
        previewLegsParent.SetActive(false);

        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        itemCost.text = string.Empty;
    }

    internal void SetPreview(Item item)
    {
        previewHeadParent.SetActive(false);
        previewTopParent.SetActive(false);
        previewLegsParent.SetActive(false);

        itemName.text = item.ItemName;
        itemDescription.text = item.ItemDescription;
        itemCost.text = "Cost: " + item.Price;

        switch (item.ItemType)
        {
            case Item.TypesOfItems.Head:

                Image[] previewHead = previewHeadParent.GetComponentsInChildren<Image>();

                for (int i = 0; i < item.Sprites.Length; i++)
                {
                    previewHead[i].sprite = item.Sprites[i];
                }

                previewHeadParent.SetActive(true);

                break;
            case Item.TypesOfItems.Torso:

                Image[] previewTop = previewTopParent.GetComponentsInChildren<Image>();

                for (int i = 0; i < item.Sprites.Length; i++)
                {
                    previewTop[i].sprite = item.Sprites[i];
                }

                previewTopParent.SetActive(true);


                break;
            case Item.TypesOfItems.Legs:

                Image[] previewLegs = previewLegsParent.GetComponentsInChildren<Image>();

                for (int i = 0; i < item.Sprites.Length; i++)
                {
                    previewLegs[i].sprite = item.Sprites[i];
                }

                previewLegsParent.SetActive(true);

                break;
        }

       
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
