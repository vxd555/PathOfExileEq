using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Eqipment : MonoBehaviour, IDropHandler
{

    public bool[] eqipSlot;
    

    public void OnDrop(PointerEventData eventData)
    {
       
    }

    public bool findFirstValidSlot(Item item)
    {
        for(int i=0; i<eqipSlot.Length; ++i)
        {
            if(!eqipSlot[i])
            {
                bool avi = false;
                for(int j=0; j<item.heigth;++j)
                {
                    if (i + j > eqipSlot.Length)
                    {
                        avi = false;
                        break;
                    }
                    if (i / 5 != (i + j) / 5 && eqipSlot[i+j])
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
                    item.slotID = i;
                    avi = true;
                }
                if (avi)
                    return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
