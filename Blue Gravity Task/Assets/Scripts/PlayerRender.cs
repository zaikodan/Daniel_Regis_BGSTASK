using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] headSprites, topSprites, legsSprites;

    internal void SetEquipment(Item item)
    {
        switch (item.ItemType)
        {
            case Item.TypesOfItems.Head:

                for (int i = 0; i < item.Sprites.Length; i++)
                {
                    headSprites[i].sprite = item.Sprites[i];
                }

                break;
            case Item.TypesOfItems.Torso:

                for (int i = 0; i < item.Sprites.Length; i++)
                {
                    topSprites[i].sprite = item.Sprites[i];
                }

                break;
            case Item.TypesOfItems.Legs:

                for (int i = 0; i < item.Sprites.Length; i++)
                {
                    legsSprites[i].sprite = item.Sprites[i];
                }

                break;
        }


    }
}
