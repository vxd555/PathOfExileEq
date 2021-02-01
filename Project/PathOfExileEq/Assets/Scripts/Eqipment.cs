using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Eqipment : MonoBehaviour, IDropHandler
{

    public bool[] eqipSlot;
    [SerializeField] public GameObject fullInfo;


    public void OnDrop(PointerEventData eventData)
    {
       
    }

    public void Stay()
    {
        fullInfo.SetActive(false);
    }

    public void Drop(GameObject item)
    {
        item.GetComponent<Item>().equiped = false;
        item.transform.localScale = new Vector3(0.2f, 0.2f);
        item.SetActive(false);
        fullInfo.SetActive(false);
    }

    public bool FindFirstValidSlot(Item item)
    {
        for(int i=0; i<eqipSlot.Length; ++i)
        {
            if(!eqipSlot[i])
            {
                bool avi = false;
                for(int j=0; j<item.heigth;++j)
                {
                    item.slotID = i;
                    avi = true;

                    if (i + j > eqipSlot.Length)
                    {
                        avi = false;
                        break;
                    }
                    if (i / 5 != (i + j) / 5 )
                    {
                        avi = false;
                        break;
                    }

                    if(eqipSlot[i+j])
                    {
                          avi = false;
                          break;  
                    }
                    if (item.width == 2 && i + j + 5 > eqipSlot.Length)
                    {
                        avi = false;
                        break;
                    }
                    if (item.width == 2 && i+j+5<eqipSlot.Length)
                        if(eqipSlot[i + j + 5])
                        {
                            avi = false;
                            break;
                        }
                    
                }
                if (avi)
                    return true;
            }
        }
        return false;
    }
}
