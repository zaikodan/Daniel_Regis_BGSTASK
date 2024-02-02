using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    enum ItemType { Head, Torso, Legs}
    [SerializeField] private ItemType itemType;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool equipable;
    [SerializeField] private int price;

    private ItemType ItemType1 { get => itemType;  }
    public Sprite[] Sprites { get => sprites; }
    public bool Equipable { get => equipable; }
    public int Price { get => price; }
}
