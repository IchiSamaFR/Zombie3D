using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public objectsInGame itemPref;
    
    

    [Header("keyBind")]
    public string openKey = "e";
    public List<string> selectSlot = new List<string>(new string[] {"&", "é"});


    [Header("Inventory")]
    public GameObject panelInventory;
    public GameObject panelInventoryHand;
    bool isOpen = false;


    [Header("statsInventory")]
    public GameObject slotObj;
    public List<GameObject> slotsLst = new List<GameObject>();
    public List<GameObject> handSlotLst = new List<GameObject>();
    public int slots = 23;
    public int unlocked = 6;


    [Header("statsOthersInventory")]
    BoxInventory newBoxInventory;
    public GameObject savedSlot;



    // Start is called before the first frame update
    void Start()
    {
        instantiateInventory();

        panelInventory.SetActive(isOpen);

        unlocked += 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            if (isOpen)
            {
                CloseInv();
            } else
            {
                OpenInv();
            }
        }
    }

    public void OpenInv()
    {

        GameObject.Find("MainCamera").GetComponent<CameraMovement>().inventoryIsOpen = true;

        isOpen = true;
        panelInventory.SetActive(true);

        RefreshInventory();
    }
    public void CloseInv()
    {

        for (int i = 0; i < slotsLst.Count; i++)
        {
            slotsLst[i].GetComponent<Slot>().deleteStats();
        }


        GameObject.Find("MainCamera").GetComponent<CameraMovement>().inventoryIsOpen = false;


        isOpen = false;
        panelInventory.SetActive(false);
    }

    void instantiateInventory()
    {
        int row = 0;
        int column = 0;

        for (int i = 0; i < slots; i++)
        {
            if (row % 2 == 0)
            {

                GameObject Slot = Instantiate(slotObj, panelInventory.transform);
                Slot.GetComponent<Slot>().inventory = this;
                float s = panelInventory.transform.localScale.x;


                /* Transform position 
                 * X : On rajoute une moitié de slot + Num Colonne * largeur du slot + La position du parent - la taille du parent
                 * Y : On enleve une moitié de slot +(pour monter et - pour descendre / Rotation) Num de ligne * Demi-Slot + La position du parent - la taille du parent
                 * Z : Rien étant la profondeur
                 */
                Slot.transform.position = new Vector3((205 * s) + column * (410 * s) + panelInventory.transform.position.x - (panelInventory.transform.GetComponent<RectTransform>().sizeDelta.x * s),
                                                    -(205 * s) - row * (205 * s) + panelInventory.transform.position.y,
                                                    0);

                //Changement de nom pour reconnaitre
                Slot.name = "Slot_" + column + ":" + row;

                //Instance du nombre de colonnes lorsque Paire
                column++;
                if (column == 5)
                {
                    row++;
                    column = 0;
                }


                slotsLst.Add(Slot);
                Slot.SetActive(true);
            } else
            {

                GameObject Slot = Instantiate(slotObj, panelInventory.transform);
                float s = panelInventory.transform.localScale.x;

                /* Transform position 
                 * X : On rajoute une moitié de slot + Num Colonne * largeur du slot + La position du parent - la taille du parent
                 * Y : On enleve une moitié de slot -(+ pour monter et - pour descendre / Rotation) Num de ligne * Demi-Slot + La position du parent
                 * Z : Rien étant la profondeur
                 */
                Slot.transform.position = new Vector3(((410 * s) + column * (410 * s)) + panelInventory.transform.position.x - (panelInventory.transform.GetComponent<RectTransform>().sizeDelta.x * s),
                                                    -(205 * s) - row * (205 * s) + panelInventory.transform.position.y,
                                                    0);

                //Changement de nom pour reconnaitre
                Slot.name = "Slot_" + column + ":" + row;


                //Instance du nombre de colonnes lorsque Impaire
                column++;
                if (column == 4)
                {
                    row++;
                    column = 0;
                }


                slotsLst.Add(Slot);
                Slot.SetActive(true);
            }
        }

        for (int i = 0; i < panelInventoryHand.transform.childCount; i++)
        {
            handSlotLst.Add(panelInventoryHand.transform.GetChild(i).gameObject);
        }
        RefreshInventory();
    }

    void RefreshInventory()
    {
        //Pour tous les slots
        for (int i = 0; i < slotsLst.Count; i++)
        {
            //Verification s'il est bien unlock et que le slot est soit (locked ou null)
            if (i < unlocked && (slotsLst[i].GetComponent<Slot>().itemInSlot.getId() == itemPref.obj[0].getId() || slotsLst[i].GetComponent<Slot>().itemInSlot.getId() == ""))
            {
                slotsLst[i].GetComponent<Slot>().Set(itemPref.obj[0]);
            } else if (i >= unlocked)
            {
                slotsLst[i].gameObject.GetComponent<Slot>().Set(itemPref.obj[1]);
            }
        }
    }

    public void oldMoveSlot(GameObject oldSlot)
    {
        savedSlot = oldSlot;
    }


    public void newMoveSlot(GameObject newSlot)
    {
        //If there is no old Slot return
        if (!savedSlot)
        {
            return;
        }

        /*Create an object to make the link
         * New object set as 0
         * 0 set as 1
         * 1 set as New object
         */
        objectsInGame.item Saved = new objectsInGame.item();

        Saved.set(newSlot.GetComponent<Slot>().getItem());
        newSlot.GetComponent<Slot>().Set(savedSlot.GetComponent<Slot>().getItem());
        savedSlot.GetComponent<Slot>().Set(Saved);
        
        
        savedSlot = null;
    }

    public void resetSaveSlot()
    {
        savedSlot = null;
    }




    #region --SET--

    public void setBoxInventory(BoxInventory newBox)
    {
        newBoxInventory = newBox;
    }

    #endregion

    #region --GET--

    public BoxInventory getBoxInventory()
    {
        return newBoxInventory;
    }

    #endregion
    

}