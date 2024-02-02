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
    [SerializeField] private bool equipable;
    [SerializeField] private int price;

    public string ItemName { get => itemName; }
    public string ItemDescription { get => itemDescription; }
    public Sprite ItemIcon { get => itemIcon; }
    public TypesOfItems ItemType { get => itemType;  }
    public Sprite[] Sprites { get => sprites; }
    public bool Equipable { get => equipable; }
    public int Price { get => price; }
    
}
