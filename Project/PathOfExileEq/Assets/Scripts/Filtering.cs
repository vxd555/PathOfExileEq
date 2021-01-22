using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filtering : MonoBehaviour
{
    [SerializeField]
    private Image[] FiltersImages;
    [SerializeField]
    private Color originalColor = Color.white;
    [SerializeField]
    private Color grayedColor = Color.gray;
    [SerializeField]
    private Color choosenColor = Color.cyan;

    private ItemTypeEnum choosenFilterType = ItemTypeEnum.None;
    private Image choosenFilterImage = null;

    public void FilterItems(string typeString)
    {
        ItemTypeEnum previousType = choosenFilterType;
        UnfilterAll();
        ItemTypeEnum newType = (ItemTypeEnum)Enum.Parse(typeof(ItemTypeEnum), typeString);
        if (previousType != newType)
            FilterItemsByType(newType);
    }

    private void UnfilterAll()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NotEquippedItem");
        choosenFilterType = ItemTypeEnum.None;
        if (choosenFilterImage != null)
            choosenFilterImage.color = originalColor;
        choosenFilterImage = null;
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Image>().color = originalColor;
        } 
    }

    private void FilterItemsByType(ItemTypeEnum type)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NotEquippedItem");
        choosenFilterType = type;
        switch(choosenFilterType)
        {
            case ItemTypeEnum.Weapon:
                {
                    choosenFilterImage = FiltersImages[0];
                    choosenFilterImage.color = choosenColor;
                } break;
            case ItemTypeEnum.Helmet:
                {
                    choosenFilterImage = FiltersImages[1];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Armor:
                {
                    choosenFilterImage = FiltersImages[2];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Belt:
                {
                    choosenFilterImage = FiltersImages[3];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Glove:
                {
                    choosenFilterImage = FiltersImages[4];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Boots:
                {
                    choosenFilterImage = FiltersImages[5];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Ring:
                {
                    choosenFilterImage = FiltersImages[6];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Amulet:
                {
                    choosenFilterImage = FiltersImages[7];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Money:
                {
                    choosenFilterImage = FiltersImages[8];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Potion:
                {
                    choosenFilterImage = FiltersImages[9];
                    choosenFilterImage.color = choosenColor;
                }
                break;
            case ItemTypeEnum.Gem:
                {
                    choosenFilterImage = FiltersImages[10];
                    choosenFilterImage.color = choosenColor;
                }
                break;
        }
        foreach (GameObject obj in objects)
        {
            ItemType itemType = obj.GetComponent<ItemType>();
            Image itemImage = obj.GetComponent<Image>();
            if (itemType.type == choosenFilterType)
            {
                itemImage.color = originalColor;
            }
            else
            {
                itemImage.color = grayedColor;
            }
        }
    }
}
