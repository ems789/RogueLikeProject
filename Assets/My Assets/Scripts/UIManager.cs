using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public GameObject dropTooltip;
    public GameObject equipTooltip;
    private int equipIdx;
    private Text dropTooltipText;
    private Text equipTooltipText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        dropTooltipText = dropTooltip.GetComponentInChildren<Text>();
        equipTooltipText = equipTooltip.GetComponentInChildren<Text>();
    }

    public void ShowDropTooltip(Vector3 position, Item _item)
    {
        dropTooltip.SetActive(true);
        dropTooltip.transform.position = position;
        dropTooltipText.text = _item.itemName + "\n\n";
        if (_item.atk > 0)
            dropTooltipText.text += "공격력 : " + _item.atk.ToString() + "\n";
        if (_item.def > 0)
            dropTooltipText.text += "방어력 : " + _item.def.ToString() + "\n";
        if (_item.hp > 0)
            dropTooltipText.text += "체력 : " + _item.hp.ToString() + "\n";
        if (_item.heal > 0)
            dropTooltipText.text += "회복량 : " + _item.heal.ToString() + "\n";
    }

    public void ShowEquipTooltip(Vector3 position, Item _item)
    {
        equipTooltip.SetActive(true);
        equipTooltip.transform.position = position;
        equipTooltipText.text = _item.itemName + "\n\n";
        if (_item.atk > 0)
            equipTooltipText.text += "공격력 : " + _item.atk.ToString() + "\n";
        if (_item.def > 0)
            equipTooltipText.text += "방어력 : " + _item.def.ToString() + "\n";
        if (_item.hp > 0)
            equipTooltipText.text += "체력 : " + _item.hp.ToString() + "\n";
        if (_item.heal > 0)
            equipTooltipText.text += "회복량 : " + _item.heal.ToString() + "\n";
    }

    public void HideTooltip()
    {
        dropTooltip.SetActive(false);
        equipTooltip.SetActive(false);
    }

}
