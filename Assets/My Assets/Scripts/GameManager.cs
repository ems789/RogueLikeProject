using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private DungeonManager dungeonScript;
    private MonsterManager monsterScript;
    public Text eventText;

    private void Awake()
    {
        if (instance == null)
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

    private IEnumerator ExitOpenText()
    {
        eventText.color = Color.red;
        eventText.text = "보스존이 열렸습니다.";

        eventText.enabled = true;
        yield return new WaitForSeconds(4f);
        eventText.enabled = false;
    }
}
