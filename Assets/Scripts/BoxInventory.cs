using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInventory : MonoBehaviour
{
    [SerializeField] private string interact = "e";


    [SerializeField] private objectsInGame items;
    [SerializeField] private GameObject panelBox;
    [SerializeField] private GameObject slotPref;
    public PlayerInventory Inventory;

    [SerializeField] private int slots = 5;

    [Header("Generation de slots")]
    public List<GameObject> lstSlots;

    [Header("Generation de loot")]
    [SerializeField] private bool isRandom = true;
    public List<string> randomPossible;
    public List<float> randomPercent;

    [Header("loots")]
    public List<objectsInGame.item> lstObj;

    public bool canOpen;
    public bool isOpen;

    
    void Start()
    {
        if(isRandom)
            loadLoot();
    }

    void Update()
    {
        if (Input.GetKeyDown(interact) && canOpen && !isOpen)
        {
            Open();
        }
        else if(!canOpen && isOpen || (isOpen && Input.GetKeyDown(interact)))
        {
            Inventory.CloseInv();
            Close();
        } else if (isOpen && Input.GetKeyDown(Inventory.openKey))
        {
            Close();
        }
    }

    public void Open()
    {
        Inventory.OpenInv();
        Inventory.setBoxInventory(this);


        isOpen = true;



        int row = 0;
        int column = 0;

        for (int i = 0; i < slots; i++)
        {
            panelBox.SetActive(true);

            if (row % 2 == 0)
            {

                GameObject Slot = Instantiate(slotPref, panelBox.transform);
                float s = panelBox.transform.localScale.x;


                /* Transform position 
                 * X : On rajoute une moitié de slot + Num Colonne * largeur du slot + La position du parent - la taille du parent
                 * Y : On enleve une moitié de slot +(pour monter et - pour descendre / Rotation) Num de ligne * Demi-Slot + La position du parent - la taille du parent
                 * Z : Rien étant la profondeur
                 */
                Slot.transform.position = new Vector3((205 * s) + column * (410 * s) + panelBox.transform.position.x,
                                                    -(205 * s) - row * (205 * s) + panelBox.transform.position.y,
                                                    0);

                //Changement de nom pour reconnaitre
                Slot.name = "Slot_" + column + ":" + row;

                //Instance du nombre de colonnes lorsque Paire
                column++;
                if (column == 4)
                {
                    row++;
                    column = 0;
                }


                lstSlots.Add(Slot);
                Slot.SetActive(true);
                Slot.GetComponent<Slot>().Set(lstObj[i]);
            }
            else
            {
                GameObject Slot = Instantiate(slotPref, panelBox.transform);
                float s = panelBox.transform.localScale.x;

                /* Transform position 
                 * X : On rajoute une moitié de slot + Num Colonne * largeur du slot + La position du parent - la taille du parent
                 * Y : On enleve une moitié de slot -(+ pour monter et - pour descendre / Rotation) Num de ligne * Demi-Slot + La position du parent
                 * Z : Rien étant la profondeur
                 */
                Slot.transform.position = new Vector3(((410 * s) + column * (410 * s)) + panelBox.transform.position.x,
                                                    -(205 * s) - row * (205 * s) + panelBox.transform.position.y,
                                                    0);

                //Changement de nom pour reconnaitre
                Slot.name = "Slot_" + column + ":" + row;


                //Instance du nombre de colonnes lorsque Impaire
                column++;
                if (column == 3)
                {
                    row++;
                    column = 0;
                }


                lstSlots.Add(Slot);
                Slot.SetActive(true);
                Slot.GetComponent<Slot>().Set(lstObj[i]);
            }
        }
    }

    public void Close()
    {

        isOpen = false;
        Inventory.setBoxInventory(null);

        lstObj = new List<objectsInGame.item>();


        for (int i = 0; i < lstSlots.Count; i++)
        {
            lstSlots[i].GetComponent<Slot>().deleteStats();
            lstObj.Add(new objectsInGame.item());
            lstObj[i].set(lstSlots[i].GetComponent<Slot>().getItem());

            Destroy(lstSlots[i]);
        }

        lstSlots = new List<GameObject>();
    }

    public void Set(objectsInGame.item itemToChange, objectsInGame.item becameItem)
    {
        itemToChange.set(becameItem);
    }

    void loadLoot()
    {
        for (int i = 0; i < slots; i++)
        {
            lstObj.Add(new objectsInGame.item());

            int rand = Random.Range(0, 100);
            int index = -1;
            float randomCount = 0;
            for (int x = 0; x < randomPossible.Count; x++)
            {
                randomCount += randomPercent[x];
                if (randomCount >= rand && index == -1)
                {
                    index = x;
                }
            }

            if (index == -1)
            {
                lstObj[i].set(items.obj[0]);
            }
            else
            {
                lstObj[i].set(items.obj[items.getItem(randomPossible[index])]);
            }
        }
    }

    
}
