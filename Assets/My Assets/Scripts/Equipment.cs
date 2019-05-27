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
    public Item[] equip = new Item[(int)EquipmentKind.Last];
    int equipIdx = 0;
    bool isEquip = false;
    Item temp;
    Image slot;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    IEnumerator TurnOffEquip()
    {
        yield return new WaitForSeconds(0.3f);
        isEquip = false;
    }

    public Item EquipItem(Item item)
    {
        if (item.image.name.Contains("Weapon"))
            equipIdx = (int)EquipmentKind.Weapon;

        if (item.image.name.Contains("Helmet"))
            equipIdx = (int)EquipmentKind.Helmet;

        if (item.image.name.Contains("Armor"))
            equipIdx = (int)EquipmentKind.Armor;

        if (item.image.name.Contains("Gloves"))
            equipIdx = (int)EquipmentKind.Gloves;

        if (item.image.name.Contains("Boots"))
            equipIdx = (int)EquipmentKind.Boots;

        slot = transform.GetChild(equipIdx).GetChild(0).GetComponent<Image>();

        // 장비창에 교체할 아이템 삽입
        if (equip[equipIdx].image == null)
        {
            equip[equipIdx] = item;
            // 캐릭터가 들고 있는 무기 이미지를 교체
            if(equipIdx == (int)EquipmentKind.Weapon)
                player.transform.Find("WeaponPosition(Front) (2)").GetComponent<SpriteRenderer>().sprite = item.image;
        }
        else
        {
            if (!isEquip)
            {
                temp = equip[(int)EquipmentKind.Weapon];
                equip[(int)EquipmentKind.Weapon] = item;
                if (equipIdx == (int)EquipmentKind.Weapon)
                    player.transform.Find("WeaponPosition(Front) (2)").GetComponent<SpriteRenderer>().sprite = item.image;
            }
        }
        isEquip = true;
        StartCoroutine("TurnOffEquip");

        // 장비창 슬릇에 이미지 삽입
        slot.sprite = item.image;
        slot.enabled = true;
        return temp;
    }
}
