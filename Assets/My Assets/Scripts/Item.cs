using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumption,
}

[System.Serializable]
public class Item
{
    public string itemName;
    public int heal;
    public int atk;
    public int def;
    public int hp;
    public ItemType itemType;
    
    public Sprite image;

    public Item(Sprite _image, string _itemName, int _heal, ItemType _itemType, int _atk=0, int _def=0, int _hp=0)
    {
        image = _image;
        itemName = _itemName;
        heal = _heal;
        itemType = _itemType;
        atk = _atk;
        def = _def;
        hp = _hp;        
    }   
}