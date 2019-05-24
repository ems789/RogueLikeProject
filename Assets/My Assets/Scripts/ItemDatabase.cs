using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();
    GameObject item;

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
        item = new GameObject("temp");
            
        Add("SetBWeaponBlade3", "동검", 0, ItemType.Equipment, 2);
        Add("SetCWeaponBlade4", "금검", 0, ItemType.Equipment, 3);
        Add("SetJWeaponBlade4", "철검", 0, ItemType.Equipment, 4);
        Add("SetLWeaponBlade4", "강철검", 0, ItemType.Equipment, 5);
    }

    void Add(string _spriteName, string _name, int _heal,ItemType _itemType, int _atk = 0, int _def = 0, int _maxHp = 0)
    {
        items.Add(new Item(Resources.Load<SpriteAtlas>("Item/ItemAtlas").GetSprite(_spriteName), _name, _heal, _itemType ,_atk, _def, _maxHp));
    }

    public void RandomItem(Transform tr)
    {
        Item temp;

        temp = items[Random.Range(0, items.Count)];

        // 게임 오브젝트에 아이템 정보를 복사
        item.name = temp.itemName;
        item.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        item.AddComponent<SpriteRenderer>().sprite = temp.image;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
        item.AddComponent<BoxCollider2D>().isTrigger = true;
        item.SetActive(true);
        item.transform.position = tr.position;
    }
}
