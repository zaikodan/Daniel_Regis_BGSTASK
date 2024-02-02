using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Button buyButton, sellButton, npcButton, inventoryButton;
    [SerializeField] GameObject buyWindow, sellWindow, shopWindow;
    [SerializeField] GameObject shopButtonPrefab;

    ShopManager shopManager;

    private void Awake()
    {
        shopManager = GetComponent<ShopManager>();
        InitializeButtons();
    }

    internal void GenerateButtons()
    {
        for (int i = 0; i < shopManager.Npc.ItemsForSale.Length; i++)
        {
            GameObject buttonInstantiated = Instantiate(shopButtonPrefab);
            buttonInstantiated.transform.SetParent(buyWindow.transform.GetChild(0), false);
            buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => shopManager.SelectItem(i));
        }

        for (int i = 0; i < shopManager.Inventory.ItemsList.Count; i++)
        {
            GameObject buttonInstantiated = Instantiate(shopButtonPrefab);
            buttonInstantiated.transform.SetParent(sellWindow.transform.GetChild(0), false);
            buttonInstantiated.GetComponent<Button>().onClick.AddListener(() => shopManager.SelectItem(i));
        }
    }

    private void InitializeButtons()
    {
        buyButton.onClick.AddListener(shopManager.BuyItem);
        //sellButton.onClick.AddListener(shopManager.SellItem);
        npcButton.onClick.AddListener(() => SwitchWindows(true));
        inventoryButton.onClick.AddListener(() => SwitchWindows(false));
    }

    private void SwitchWindows(bool buying)
    {
        buyWindow.SetActive(buying);
        sellWindow.SetActive(!buying);
    }

    internal void OpenShop()
    {
        shopWindow.SetActive(true);
    }
}
