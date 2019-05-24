using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite openSprite;
    SpriteRenderer spriteRenderer;
    GameObject item;
    Item temp;

    private bool isOpen = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        item = new GameObject("temp");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Open();
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            temp = ItemDatabase.instance.RandomItem();
            Debug.Log(temp.itemName);

            item.name = temp.itemName;
            item.AddComponent<SpriteRenderer>().sprite = temp.image;
            item.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
            item.AddComponent<BoxCollider2D>().isTrigger = true;
            item.SetActive(true);
            item.transform.position = transform.position;
        }
    }
}
