using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> itemsList = new List<Item>();
    int money = 100;

    public List<Item> ItemsList { get => itemsList; }

    public int Money { get => money; set => money = value; }
}
