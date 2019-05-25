using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    Item item;
    Equipment equip;

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(","))
        {
            if (item.itemType == ItemType.Equipment)
            {
                equip.EquipItem(item);
            }
            else if (item.itemType == ItemType.Consumption)
            {
                Debug.Log("소비");
            }
        }
    }
}
