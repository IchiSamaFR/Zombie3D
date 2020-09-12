using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHand : Slot
{
    public Text ammunition;

    void Start()
    {
        Set(inventory.itemPref.obj[0]);
        this.Refresh();
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(0).GetComponent<Image>().sprite = itemInSlot.getSprite();
    }
    void Update()
    {
        mouseMovement();
        loaderRefresh();
    }

    public void loaderRefresh()
    {
        if (this.getItem().ammoMax != 0)
        {
            ammunition.text = getItem().ammo + " / " + getItem().ammoMax;
        } else
        {
            ammunition.text = "";
        }
    }
    
}
