using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        //Add("Club", "몽둥이", 0, ItemType.Equipment, 1);        
        Add("SetBWeaponBlade3", "동검", 0, ItemType.Equipment, 2);
        Add("SetCWeaponBlade4", "금검", 0, ItemType.Equipment, 3);
        Add("SetJWeaponBlade4", "철검", 0, ItemType.Equipment, 4);
        Add("SetLWeaponBlade4", "강철검", 0, ItemType.Equipment, 5);
    }

    void Add(string _spriteName, string _name, int _heal,ItemType _itemType, int _atk = 0, int _def = 0, int _maxHp = 0)
    {
        items.Add(new Item(Resources.Load<SpriteAtlas>("Item/ItemAtlas").GetSprite(_spriteName), _name, _heal, _itemType ,_atk, _def, _maxHp));
    }
}
