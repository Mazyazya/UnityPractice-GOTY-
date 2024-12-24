using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script: MonoBehaviour
{
    public GameObject dialogMenu;
    public GameObject pref;
    private GameObject obj;
    private DialogueManager dialogueManager;
    private Dictionary<int, List<string>> arrOfActs;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = dialogMenu.GetComponent<DialogueManager>();
        dialogueManager.LoadDialogues();
        MinigameScript.onCompliete += onComplite;
        arrOfActs = new Dictionary<int, List<string>>() {
            {1, new List<string>(){ "dialog", "home" } },
            {2, new List<string>(){ "dialog", "shelter" } },
            {3, new List<string>(){ "dialog", "home_return" } },
            {4, new List<string>(){ "minigame", "" } },
            {5, new List<string>(){ "dialog", "after_minigame" } },
            {6, new List<string>(){ "game", "300" } },
            {7, new List<string>(){ "dialog", "home_again" } },
            {8, new List<string>(){ "dialog", "lesson" } },
            {9, new List<string>(){ "game", "1500" } } };

        NextAct();
    }

    public void NextAct()
    {
        Execute(arrOfActs[StaticData.currentAct]);
        StaticData.currentAct++;
    }
    private void Execute(List<string> commands)
    {
        if (commands[0] == "dialog")
        {
            dialogueManager.DisplayScene(commands[1]);
        }
        else if (commands[0] == "game") {
            SceneManager.LoadScene("Game");
        }
        else {
            var obj = Instantiate(pref, new Vector3(0, -10, 0), Quaternion.identity);
        };
    }
    private void onComplite()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Dialogs")
        {
            NextAct();
        }
    }
}
