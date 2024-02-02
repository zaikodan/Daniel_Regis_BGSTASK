using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum TypesOfItems { Head, Torso, Legs}

    [SerializeField] string itemName;
    [TextArea, SerializeField] string itemDescription;
    [SerializeField] Sprite itemIcon;
    [SerializeField] private TypesOfItems itemType;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool equiped, equipable;
    [SerializeField] private int price;

    public void SetItem(Item _item)
    {
        itemName = _item.itemName;
        itemDescription = _item.itemDescription;
        itemIcon = _item.itemIcon;
        itemType = _item.itemType;
        sprites = _item.sprites;
        equiped = _item.equiped;
        equipable = _item.equipable;
        price = _item.price;

        name = itemName;
    }

    public string ItemName { get => itemName; }
    public string ItemDescription { get => itemDescription; }
    public Sprite ItemIcon { get => itemIcon; }
    public TypesOfItems ItemType { get => itemType;  }
    public Sprite[] Sprites { get => sprites; }
    public bool Equiped { get => equiped; set => equiped = value; }
    public bool Equipable { get => equipable; }
    public int Price { get => price; }
    
}
