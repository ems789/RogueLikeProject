using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    private Item temp;
    SpriteRenderer itemSprite;
    Equipment equip;
    int equipIdx;
    Vector3 slotPos;
    bool isPickUp = false;

    private void Start()
    {
        equip = FindObjectOfType<Equipment>();
    }

    public void Init(Item temp)
    {
        item = temp;

        gameObject.name = item.itemName;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        gameObject.AddComponent<SpriteRenderer>().sprite = item.image;        
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;

        itemSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "MyHero")
            UIManager.instance.HideTooltip();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "MyHero")
        {
            UIManager.instance.ShowDropTooltip(Camera.main.WorldToScreenPoint(transform.position), item); // ui화면에 맞춘 포지션   
            if (equip.SameTypeReturn(item) != null) // 착용한 아이템이 있는 경우
            {
                temp = equip.SameTypeReturn(item);
                equipIdx = equip.TypeCheck(item);
                slotPos = equip.SlotTrans();
                UIManager.instance.ShowEquipTooltip(slotPos, temp);
            }
        }

        if (Input.GetKeyDown(","))
        {
            if (!isPickUp)
            {
                Debug.Log("줍기");
                UIManager.instance.HideTooltip();
                if (item.itemType == ItemType.Equipment)
                {
                    if (equip.EquipItem(item) == null) // 착용한 아이템이 없는 경우      
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        item = equip.EquipItem(item);
                        itemSprite.sprite = item.image;
                    }
                }
                else if (item.itemType == ItemType.Consumption)
                {
                    Debug.Log("소비");
                }
                isPickUp = true;
                StartCoroutine("PickUp");
            }
        }
    }

    IEnumerator PickUp()
    {
        yield return new WaitForSeconds(0.3f);
        isPickUp = false;
    }
}
