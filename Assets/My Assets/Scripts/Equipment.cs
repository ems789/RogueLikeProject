using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public enum EquipmentKind
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
    public Image slot;

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

        if (!isEquip) // 중복 호출 방지
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

            // 착용한 장비 해제
            if (equip[equipIdx].image != null)
            {
                temp = equip[equipIdx];
                equip[(int)EquipmentKind.Weapon] = item;

                Player.instance.atk -= temp.atk;
                Player.instance.def -= temp.def;
                Player.instance.hp -= temp.hp;
                if (Player.instance.currentHP > Player.instance.hp)
                    Player.instance.currentHP = Player.instance.hp;
            }
            // 장비 장착
            equip[equipIdx] = item;

            // 캐릭터가 들고 있는 무기 이미지를 교체
            if (equipIdx == (int)EquipmentKind.Weapon)
                player.transform.Find("WeaponPosition(Front) (2)").GetComponent<SpriteRenderer>().sprite = item.image;
            // 장비 슬릇 교체
            slot.sprite = item.image;
            slot.enabled = true;

            Player.instance.atk += item.atk;
            Player.instance.def += item.def;
            Player.instance.hp += item.hp;
        }   
        isEquip = true;
        StartCoroutine("TurnOffEquip");

        return temp; // 착용중인 아이템 반환
    }
}
