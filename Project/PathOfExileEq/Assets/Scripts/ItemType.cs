using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeEnum
{
    None,
    Weapon,
    Helmet,
    Armor,
    Belt,
    Glove,
    Boots,
    Ring,
    Amulet,
    Money,
    Potion,
    Gem
};

public class ItemType : MonoBehaviour
{
    public ItemTypeEnum type;
}
