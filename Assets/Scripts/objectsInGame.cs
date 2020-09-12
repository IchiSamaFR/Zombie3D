using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsInGame : MonoBehaviour
{

    [Header("Objects")]
    public Sprite objNull;
    public List<item> obj = new List<item>();
    public List<blocked> lstBlocked = new List<blocked>();
    public List<weapon> lstWeapons = new List<weapon>();
    public List<heal> lstHeal = new List<heal>();


    void Awake(){
        foreach(blocked blockedGet in lstBlocked){
            obj.Add(new item());
            obj[obj.Count - 1].set(blockedGet);
        }

        foreach(weapon weaponGet in lstWeapons){
            obj.Add(new item());
            obj[obj.Count - 1].set(weaponGet);
        }

        foreach(heal healGet in lstHeal){
            obj.Add(new item());
            obj[obj.Count - 1].set(healGet);
        }
    }


    [System.Serializable]
    public class item
    {
        [Header("General Stats")]
        public string id;
        public string nameItem;
        public Sprite sprite;
        public string type;

        [Header("Weapon Stats")]
        public int ammo = 0;
        public int ammoMax = 0;

        public float dmg = 0;
        public float fireRate = 0;

        
        [Header("Usable Stats")]
        public int uses = 0;
        public int maxUses = 0;


        [Header("Heal Stats")]
        public int gainHeal = 0;

        [Header("Stamina Stats")]
        public int gainStamina = 0;

        [Header("Food Stats")]
        public int gainFood = 0;

        [Header("Water Stats")]
        public int gainWater = 0;


        //Set, recupere chaque valeur et les enregistre
        public void set(item objectToSet)
        {
            id = objectToSet.id;
            nameItem = objectToSet.nameItem;
            sprite = objectToSet.sprite;
            type = objectToSet.type;

            ammo = objectToSet.ammo;
            ammoMax = objectToSet.ammoMax;
            dmg = objectToSet.dmg;
            fireRate = objectToSet.fireRate;

            uses = objectToSet.uses;
            maxUses = objectToSet.maxUses;

            gainHeal = objectToSet.gainHeal;
            gainStamina = objectToSet.gainStamina;
            gainFood = objectToSet.gainFood;
            gainWater = objectToSet.gainWater;
        }
        public void set(blocked objectToSet)
        {
            id = objectToSet.id;
            nameItem = objectToSet.nameItem;
            sprite = objectToSet.sprite;
            type = objectToSet.type;
        }
        public void set(weapon objectToSet)
        {
            id = objectToSet.id;
            nameItem = objectToSet.nameItem;
            sprite = objectToSet.sprite;
            type = objectToSet.type;

            ammo = objectToSet.ammo;
            ammoMax = objectToSet.ammoMax;
            dmg = objectToSet.dmg;
            fireRate = objectToSet.fireRate;
        }
        public void set(heal objectToSet)
        {
            id = objectToSet.id;
            nameItem = objectToSet.nameItem;
            sprite = objectToSet.sprite;
            type = objectToSet.type;

            uses = objectToSet.uses;
            maxUses = objectToSet.maxUses;

            gainHeal = objectToSet.gainHeal;
            gainStamina = objectToSet.gainStamina;
        }

        public string getId()
        {
            return id;
        }
        public Sprite getSprite()
        {
            return sprite;
        }
        public void shoot()
        {
            ammo--;
        }
    }

    [System.Serializable]
    public class blocked
    {
        [Header("General Stats")]
        public string id;
        public string nameItem;
        public Sprite sprite;
        public string type;

    }

    [System.Serializable]
    public class weapon
    {
        [Header("General Stats")]
        public string id;
        public string nameItem;
        public Sprite sprite;
        public string type = "weapon";

        [Header("Weapon Stats")]
        public int ammo = 0;
        public int ammoMax = 0;

        public float dmg = 0;
        public float fireRate = 0;
        
    }

    [System.Serializable]
    public class heal
    {
        [Header("General Stats")]
        public string id;
        public string nameItem;
        public Sprite sprite;
        public string type = "heal";

        
        [Header("Usable Stats")]
        public int uses = 0;
        public int maxUses = 0;


        [Header("Heal Stats")]
        public int gainHeal = 0;

        [Header("Stamina Stats")]
        public int gainStamina = 0;

    }

    public int getItem(string objId){
        
        for (int i = 0; i < obj.Count; i++){
            if(obj[i].id == objId)
                return i;
        }
        return -1;
    }

}
