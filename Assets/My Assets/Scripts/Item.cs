using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumption,
}

public class Item
{
    public string name;
    public int heal;
    public int atk;
    public int def;
    public int maxHp;

    public Sprite Image;
    

    public Item(string _name, int _heal, int _atk=0, int _def=0, int _maxHp=0, Sprite _Image)
    {
        name = _name;
        heal = _heal;
        atk = _atk;
        def = _def;
        maxHp = _maxHp;
        Image = _Image;

    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
