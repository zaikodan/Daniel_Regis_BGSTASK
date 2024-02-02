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

    internal void GenerateButtons()
    {
        if (buyContent.transform.childCount == 0)
        {
            for (int i = 0; i < shopManager.Npc.ItemsForSale.Length; i++)
            {
                int index = i;
                GameObject buttonInstantiated = Instantiate(shopButtonPrefab);
                buttonInstantiated.transform.SetParent(buyContent.transform, false);
                buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => shopManager.SelectItem(index, buttonInstantiated));
                buttonInstantiated.GetComponentInChildren<Text>().text = shopManager.Npc.ItemsForSale[i].ItemName;
            }
        }

        int amount = sellContent.transform.childCount;

            while (sellContent.transform.childCount < shopManager.Inventory.ItemsList.Count)
            {
                int index = amount;
                GameObject buttonInstantiated = Instantiate(shopButtonPrefab);
                buttonInstantiated.transform.SetParent(sellContent.transform, false);
                buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => shopManager.SelectItem(index, buttonInstantiated));
                buttonInstantiated.GetComponentInChildren<Text>().text = shopManager.Inventory.ItemsList[amount].ItemName;
            amount++;
            }
        
    }

    internal void UpdateButtons()
    {
        for(int i = 0;i<sellContent.transform.childCount;i++)
        {
            Button button = sellContent.transform.GetChild(i).GetComponent<Button>();
            int index = i;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => shopManager.SelectItem(index, button.gameObject));
            button.GetComponentInChildren<Text>().text = shopManager.Inventory.ItemsList[i].ItemName;
        }
                
        
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

    private void SwitchWindows(bool buying)
    {
        shopManager.buying = buying;
        buyWindow.SetActive(buying);
        sellWindow.SetActive(!buying);

        SetBuyButton(false);
        SetSellButton(false);
        SetPreview();
    }

    internal void SetPreview()
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
