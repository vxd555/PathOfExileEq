using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public ItemTypeEnum slotType;

    public void OnDrop(PointerEventData eventData)
    {
        
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ItemType>().type == slotType)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<Item>().equip = true;
        }
    }
}
