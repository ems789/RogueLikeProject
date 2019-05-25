using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    Item item;

    public void Init(Item temp)
    {
        item = temp;

        gameObject.name = item.itemName;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        gameObject.AddComponent<SpriteRenderer>().sprite = item.image;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }
}
