using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public PlayerInventory inventory;

    public GameObject StatsObj;

    public GameObject objMouseFollow;

    public objectsInGame.item itemInSlot = new objectsInGame.item();

    
    void Start()
    {
        Refresh();
    }

    GameObject slotFollow;
    bool needToDelete;
    Vector3 diff;
    void Update()
    {
        showStats();
        mouseMovement();
    }

    public void mouseMovement()
    {
        //If mouse in the slot and left click and left shift and the slot is not locked or null
        if (isStay() && Input.GetMouseButtonDown(0) && Input.GetKey("left shift") && itemInSlot.id != "locked" && itemInSlot.id != "null")
        {
            //if the slot is on the main inventory and a box is open
            if(inventory.slotsLst.IndexOf(this.gameObject) >= 0 && inventory.getBoxInventory() != null)
            {
                //Place this slot to the first null object find
                inventory.oldMoveSlot(this.gameObject);
                
                BoxInventory box = inventory.getBoxInventory();
                int index = -1;
                for (int i = 0; i < box.lstObj.Count; i++)
                {
                    if (box.lstObj[i].id == "null" && index == -1)
                    {
                        index = i;
                        inventory.newMoveSlot(box.lstSlots[i]);
                    }
                }
            }
            //if the slot is on the box and a box is open
            else if (inventory.getBoxInventory() != null && inventory.getBoxInventory().lstSlots.IndexOf(this.gameObject) >= 0)
            {
                //Place this slot to the first null object find
                inventory.oldMoveSlot(this.gameObject);
                
                int index = -1;
                for (int i = 0; i < inventory.slotsLst.Count; i++)
                {
                    if (inventory.slotsLst[i].GetComponent<Slot>().itemInSlot.id == "null" && index == -1)
                    {
                        index = i;
                        inventory.newMoveSlot(inventory.slotsLst[i]);
                    }
                }
            }

            else if (inventory.getBoxInventory() == null && inventory.slotsLst.IndexOf(this.gameObject) >= 0)
            {
                //Place this slot to the first null object find
                inventory.oldMoveSlot(this.gameObject);
                
                int index = -1;
                for (int i = 0; i < inventory.handSlotLst.Count; i++)
                {
                    if (inventory.handSlotLst[i].GetComponent<Slot>().itemInSlot.id == "null" && index == -1)
                    {
                        index = i;
                        inventory.newMoveSlot(inventory.handSlotLst[i]);
                    }
                }
            }

            else if (inventory.getBoxInventory() == null && inventory.handSlotLst.IndexOf(this.gameObject) >= 0)
            {
                //Place this slot to the first null object find
                inventory.oldMoveSlot(this.gameObject);
                
                int index = -1;
                for (int i = 0; i < inventory.slotsLst.Count; i++)
                {
                    if (inventory.slotsLst[i].GetComponent<Slot>().itemInSlot.id == "null" && index == -1)
                    {
                        index = i;
                        inventory.newMoveSlot(inventory.slotsLst[i]);
                    }
                }
            }



            needToDelete = true;
            deleteStats();
        }
        else if (isStay() && Input.GetMouseButtonDown(0) && itemInSlot.id != "locked" && itemInSlot.id != "null")
        {
            slotFollow = Instantiate(objMouseFollow, this.transform.parent.parent.parent);
            slotFollow.transform.localScale = this.transform.parent.localScale;
            slotFollow.transform.GetChild(0).GetComponent<Image>().sprite = itemInSlot.getSprite();
            diff = new Vector3(this.transform.position.x - Input.mousePosition.x,
                             this.transform.position.y - Input.mousePosition.y, 0);
            

            inventory.oldMoveSlot(this.gameObject);
        }
        else if (isStay() && Input.GetMouseButtonDown(0) && (itemInSlot.id == "locked" || itemInSlot.id == "null"))
        {
            inventory.resetSaveSlot();
        }
        if (Input.GetMouseButtonUp(0) && itemInSlot.id != "locked")
        {
            if (isStay())
            {
                inventory.newMoveSlot(this.gameObject);
            }
            Destroy(slotFollow);
            slotFollow = null;
        }

        if (slotFollow != null)
        {
            float s = this.transform.parent.localScale.x;
            slotFollow.transform.position = new Vector3(diff.x + Input.mousePosition.x, diff.y + Input.mousePosition.y, 0);
        }
    }


    //Show Stats when mouse in
    GameObject statsInfos;
    bool wasInto = false;
    float timer;
    public void showStats()
    {

        //Don't show stats if the slot is null or locked
        if (itemInSlot.nameItem == "locked" || itemInSlot.nameItem == "null")
        {
            return;
        }

        //if mouse into then start the timer before show stats
        if (isStay() && !wasInto)
        {
            timer = Time.time;
            wasInto = true;
        }

        //Show stats after 0.2 sec
        if (isStay() && !inventory.savedSlot && !statsInfos && Time.time >= timer + 0.2f && !slotFollow)
        {
            //Create shower stats
            statsInfos = Instantiate(StatsObj, this.transform.parent);
            //Scale it to base
            statsInfos.transform.localScale = new Vector3(1, 1, 1);

            //Get the localscale of the inventory to position the stats info correctly
            float s = this.transform.parent.localScale.x;
            statsInfos.transform.position = this.transform.position + new Vector3((this.gameObject.GetComponent<RectTransform>().sizeDelta.x + 10) * s, 0, 0);

            //Show all stats by item type
            if (itemInSlot.type == "weapon")
            {
                statsInfos.transform.GetChild(0).gameObject.SetActive(true);
                statsInfos.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = itemInSlot.nameItem;

                statsInfos.transform.GetChild(1).gameObject.SetActive(true);
                statsInfos.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Mun. Max. : " + itemInSlot.ammoMax;

                statsInfos.transform.GetChild(2).gameObject.SetActive(true);
                statsInfos.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Mun. Load : " + itemInSlot.ammo;

                statsInfos.transform.GetChild(3).gameObject.SetActive(true);
                statsInfos.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Dégats : " + itemInSlot.dmg;
            } 
            else if(itemInSlot.type == "heal") {
                statsInfos.transform.GetChild(0).gameObject.SetActive(true);
                statsInfos.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = itemInSlot.nameItem;

                statsInfos.transform.GetChild(1).gameObject.SetActive(true);
                statsInfos.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Uses : " + itemInSlot.uses;

                statsInfos.transform.GetChild(2).gameObject.SetActive(true);
                statsInfos.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Heal : " + itemInSlot.gainHeal;

                if(itemInSlot.gainStamina != 0){
                    statsInfos.transform.GetChild(3).gameObject.SetActive(true);
                    statsInfos.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Stamina : " + itemInSlot.gainStamina;
                } else {
                    statsInfos.transform.GetChild(3).gameObject.SetActive(false);
                }
            }

        }
        //else if isn't stay or is moving destroy the show stats
        else if (!isStay() || slotFollow)
        {
            wasInto = false;
            deleteStats();
        }
    }


    //Destroy show stats
    public void deleteStats()
    {
        Destroy(getStatsInfos());
    }

    bool isStay()
    {

        float s = this.transform.parent.localScale.x;
        float demi = this.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;

        /*Si la souris est dans le carre de l'image donc
         * Position de la souris >= position du rectangle en x - la moitié pour avoir le début et + la moitié pour la fin
         * Position de la souris >= position du rectangle en x - la moitié pour avoir le début et + la moitié pour la fin
         */
        if (Input.mousePosition.x >= this.transform.position.x - demi * s && Input.mousePosition.x <= this.transform.position.x + demi * s
            && Input.mousePosition.y >= this.transform.position.y - demi * s && Input.mousePosition.y <= this.transform.position.y + demi * s)
        {


            /*Si la souris est dans le carre haut gauche du rectangle (Divise en 4 pour diviser en 2 etant un losange) donc
             * Position de la souris >= position du rectangle en x Donc Droite
             * Position de la souris >= position du rectangle en Y Donc Haut
             * Donc partie en haut à droite
             * Et si la souris.x - la position.x + souris.x - la position.x > a la moitie alors c'est qu'il est dans la partie haute
             */

            if (Input.mousePosition.x <= this.transform.position.x
                && Input.mousePosition.y >= this.transform.position.y
                && -(Input.mousePosition.x - this.transform.position.x) + (Input.mousePosition.y - this.transform.position.y) <= (demi * s))
            {
                return (true);
            }
            else 
            if (Input.mousePosition.x >= this.transform.position.x
                && Input.mousePosition.y >= this.transform.position.y
                && (Input.mousePosition.x - this.transform.position.x) + (Input.mousePosition.y - this.transform.position.y) <= (demi * s))
            {
                return (true);
            }
            else
            if (Input.mousePosition.x <= this.transform.position.x
                && Input.mousePosition.y <= this.transform.position.y
                && -(Input.mousePosition.x - this.transform.position.x) + -(Input.mousePosition.y - this.transform.position.y) <= (demi * s))
            {
                return (true);
            }
            else
            if (Input.mousePosition.x >= this.transform.position.x
                && Input.mousePosition.y <= this.transform.position.y
                && (Input.mousePosition.x - this.transform.position.x) + -(Input.mousePosition.y - this.transform.position.y) <= (demi * s))
            {
                return (true);
            } else
            {
                return (false);
            }
        }
        else
        {
            return (false);
        }
    }

    //Set the item in the slot
    public void Set(objectsInGame.item itemToChange)
    {
        itemInSlot.set(itemToChange);

        Refresh();
    }



    //refresh slot sprite
    public void Refresh()
    {
        if (inventory.getBoxInventory() != null && inventory.getBoxInventory().lstSlots.IndexOf(this.gameObject) >= 0)
        {
            BoxInventory box = inventory.getBoxInventory();
            int x = box.lstSlots.IndexOf(this.gameObject);

            box.lstObj[x].set(itemInSlot);

        }
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(0).GetComponent<Image>().sprite = itemInSlot.getSprite();

    }





    #region --GET--

    //Return item in the slot
    public objectsInGame.item getItem()
    {
        return itemInSlot;
    }

    
    //Return inventory
    public PlayerInventory getInventory()
    {
        return inventory;
    }

    public GameObject getStatsInfos()
    {
        return statsInfos;
    }
    #endregion

}
