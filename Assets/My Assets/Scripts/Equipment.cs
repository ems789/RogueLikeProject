using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    const int Weapon = 0, Helmet = 1, Armor = 2, Gloves =3, Boots = 4;
    Image slot;
    
   public void EquipItem(Item item)
    {
        if(item.image.name.Contains("Weapon"))
            slot = transform.GetChild(Weapon).GetChild(0).GetComponent<Image>();
           
        if (item.image.name.Contains("Helmet"))
            slot = transform.GetChild(Helmet).GetChild(0).GetComponent<Image>();

        if (item.image.name.Contains("Armor"))
            slot = transform.GetChild(Armor).GetChild(0).GetComponent<Image>();

        if (item.image.name.Contains("Gloves"))
            slot = transform.GetChild(Gloves).GetChild(0).GetComponent<Image>();

        if (item.image.name.Contains("Boots"))
            slot = transform.GetChild(Boots).GetChild(0).GetComponent<Image>();

        slot.sprite = item.image;
        slot.enabled = true;
    }
}
