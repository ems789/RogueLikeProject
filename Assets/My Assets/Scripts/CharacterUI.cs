using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Slider hpBar;
    public GameObject headUpPosition;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        hpBar.value = (float)player.currentHP / (float)player.hp;
        hpBar.transform.position = headUpPosition.transform.position;
    }
}
