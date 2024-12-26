using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Script
{
    private static Dictionary<int, List<string>> seq = StaticData.arrOfActs;

    public static event Action OnMinigameStart;
    public static void StartGame()
    {
        MinigameScript.onCompliete += onComplite;
        NextAct();
    }

    public static void NextAct()
    {
        DataBaseManager.SaveData();
        StaticData.currentAct++;
        Execute(seq[StaticData.currentAct]);
    }
    private static void Execute(List<string> commands)
    {
        if (commands[0] == "dialog")
        {
            StaticData.currentDialog = commands[1];
            SceneManager.LoadScene("Dialogs");
        }
        else if (commands[0] == "game") {
            StaticData.currentDestination = int.Parse(commands[1]);
            SceneManager.LoadScene("Game");
        }
        else {
            OnMinigameStart.Invoke();
        };
    }
    private static void onComplite()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Dialogs")
        {
            NextAct();
        }
    }
}
