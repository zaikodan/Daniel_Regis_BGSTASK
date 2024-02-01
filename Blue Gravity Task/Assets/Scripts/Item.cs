using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    enum BodyPart { Head, Torso, Legs}
    [SerializeField] private BodyPart bodyPart;
    [SerializeField] private Sprite[] sprites;
}
