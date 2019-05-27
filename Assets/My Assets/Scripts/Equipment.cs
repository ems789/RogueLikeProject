using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    enum EquipmentKind
    {
        Weapon,
        Helmet,
        Armor,
        Gloves,
        Boots,
        Last,
    }

    GameObject player;
    Image slot;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    public void EquipItem(Item item)
    {
        if (item.image.name.Contains("Weapon"))
        {
            slot = transform.GetChild((int)EquipmentKind.Weapon).GetChild(0).GetComponent<Image>();
            // 캐릭터가 들고 있는 무기 이미지를 교체
            player.transform.Find("WeaponPosition(Front) (2)").GetComponent<SpriteRenderer>().sprite = item.image;                        
        }
           
        if (item.image.name.Contains("Helmet"))
            slot = transform.GetChild((int)EquipmentKind.Helmet).GetChild(0).GetComponent<Image>();

        if (item.image.name.Contains("Armor"))
            slot = transform.GetChild((int)EquipmentKind.Armor).GetChild(0).GetComponent<Image>();

        if (item.image.name.Contains("Gloves"))
            slot = transform.GetChild((int)EquipmentKind.Gloves).GetChild(0).GetComponent<Image>();

        if (item.image.name.Contains("Boots"))
            slot = transform.GetChild((int)EquipmentKind.Boots).GetChild(0).GetComponent<Image>();

        slot.sprite = item.image;
        slot.enabled = true;
    }
}
