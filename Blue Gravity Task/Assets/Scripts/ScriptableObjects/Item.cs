using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum TypesOfItem { Hood, Armor, Boots}

    [Header("Texts")]

    [SerializeField] 
    string itemName;
    [TextArea, SerializeField] 
    string itemDescription;

    [Header("Sprites")]

    [SerializeField] Sprite itemIcon;
    [SerializeField] private Sprite[] sprites;

    [Header("Miscellanous")]

    [SerializeField] 
    TypesOfItem itemType;
    [SerializeField] 
    int price;
    [SerializeField] 
    bool equipable;
    bool equiped;

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
    public TypesOfItem ItemType { get => itemType;  }
    public Sprite[] Sprites { get => sprites; }
    public bool Equiped { get => equiped; set => equiped = value; }
    public bool Equipable { get => equipable; }
    public int Price { get => price; }
    
}
