using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private DungeonManager dungeonScript;
    private MonsterManager monsterScript;

    private void Awake()
    {
        if (instance = null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        dungeonScript = GameObject.FindWithTag("DungeonManager").GetComponent<DungeonManager>();
        monsterScript = GameObject.FindWithTag("MonsterManager").GetComponent<MonsterManager>();

        InitGame();
    }

    private void InitGame()
    {
        dungeonScript.SetupDungeon();
        monsterScript.MonsterSetting();
    }
}
