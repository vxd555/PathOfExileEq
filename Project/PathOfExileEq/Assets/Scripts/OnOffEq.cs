using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffEq : MonoBehaviour
{
    public List<GameObject>     closeBackground     = new List<GameObject>();
    public List<GameObject>     farBackground       = new List<GameObject>();

    public int                  currentBackground   = 0;
    public bool                 backpackIsOpen      = false;
    public bool                 inventoryIsOpen     = true;

    public GameObject           backpack            = null;
    public GameObject           inventory           = null;
    public GameObject           filters             = null;
    public GameObject           items               = null;
    public GameObject           items2              = null;
    public GameObject           itemCompare         = null;


    void Start()
    {
        UpdateBG();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBackground = 0;
            UpdateBG();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentBackground = 1;
            UpdateBG();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBackground = 2;
            UpdateBG();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentBackground = 3;
            UpdateBG();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            if(backpackIsOpen)
            {
                backpackIsOpen = false;
            }
            else
            {
                backpackIsOpen = true;
            }
            
            UpdateBG();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            HideWearItem();
        }
    }

    public void HideWearItem()
    {
        if(backpackIsOpen)
        {
            inventoryIsOpen = !inventoryIsOpen;
            UpdateBG();
        }
    }

    void UpdateBG()
    {
        for(int i = 0; i < closeBackground.Count; ++i)
        {
            closeBackground[i].SetActive(false);
            farBackground[i].SetActive(false);
        }
        if(backpackIsOpen)
        {
            closeBackground[currentBackground].SetActive(true);
            backpack.SetActive(true);
            inventory.SetActive(inventoryIsOpen);
        }
        else
        {
            farBackground[currentBackground].SetActive(true);
            backpack.SetActive(false);
            inventory.SetActive(false);
        }
    }
}
