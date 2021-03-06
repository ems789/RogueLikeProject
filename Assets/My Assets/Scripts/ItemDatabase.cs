﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();
    GameObject newGameObj;

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
            
        Add("SetBWeaponBlade3", "목검", 0, ItemType.Equipment, 2);
        Add("SetDWeaponBlade3", "동검", 0, ItemType.Equipment, 3);
        Add("SetJWeaponBlade4", "철검", 0, ItemType.Equipment, 4);
        Add("SetLWeaponBlade4", "강철검", 0, ItemType.Equipment, 5);
        Add("SetCWeaponBlade4", "황금검", 0, ItemType.Equipment, 6);
    }

    void Add(string _spriteName, string _name, int _heal,ItemType _itemType, int _atk = 0, int _def = 0, int _maxHp = 0)
    {
        items.Add(new Item(Resources.Load<SpriteAtlas>("Item/ItemAtlas").GetSprite(_spriteName), _name, _heal, _itemType ,_atk, _def, _maxHp));
    }

    public void RandomItem(Transform tr)
    {
        newGameObj = new GameObject("randomItem"); // 생성할 아이템이 할당될 오브젝트 생성

        Item randomItem;
        randomItem = items[Random.Range(0, items.Count)];

        // 새로 생성한 게임 오브젝트에 아이템 정보를 복사
        newGameObj.AddComponent<ItemInfo>().Init(randomItem);
        newGameObj.transform.position = tr.position;
    }
}
