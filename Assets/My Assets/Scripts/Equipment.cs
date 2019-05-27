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
        {
            slot = transform.GetChild((int)EquipmentKind.Weapon).GetChild(0).GetComponent<Image>();
            // 들고 있는 아이템이 없는 경우
            if (equip[(int)EquipmentKind.Weapon].image == null)
            {
                equip[(int)EquipmentKind.Weapon] = item;
                // 캐릭터가 들고 있는 무기 이미지를 교체
                player.transform.Find("WeaponPosition(Front) (2)").GetComponent<SpriteRenderer>().sprite = item.image;
            }
            else
            {
                if (!isEquip)
                {
                    temp = equip[(int)EquipmentKind.Weapon];
                    equip[(int)EquipmentKind.Weapon] = item;
                    player.transform.Find("WeaponPosition(Front) (2)").GetComponent<SpriteRenderer>().sprite = item.image;
                }
            }
            isEquip = true;
            StartCoroutine("TurnOffEquip");

        }

        if (item.image.name.Contains("Helmet"))
        {
            slot = transform.GetChild((int)EquipmentKind.Helmet).GetChild(0).GetComponent<Image>();
            equip[(int)EquipmentKind.Helmet] = item;
        }

        if (item.image.name.Contains("Armor"))
        {
            slot = transform.GetChild((int)EquipmentKind.Armor).GetChild(0).GetComponent<Image>();
            equip[(int)EquipmentKind.Armor] = item;
        }


        if (item.image.name.Contains("Gloves"))
        {
            slot = transform.GetChild((int)EquipmentKind.Gloves).GetChild(0).GetComponent<Image>();
            equip[(int)EquipmentKind.Gloves] = item;
        }

        if (item.image.name.Contains("Boots"))
        {
            slot = transform.GetChild((int)EquipmentKind.Boots).GetChild(0).GetComponent<Image>();
            equip[(int)EquipmentKind.Boots] = item;
        }

        slot.sprite = item.image;
        slot.enabled = true;
        return temp;
    }
}
