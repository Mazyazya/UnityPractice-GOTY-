using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static Dictionary<int, List<string>>  arrOfActs = new Dictionary<int, List<string>>() {
            {1, new List<string>(){ "dialog", "home" } },
            { 2, new List<string>() { "dialog", "shelter" } },
            { 3, new List<string>() { "dialog", "home_return" } },
            { 4, new List<string>() { "minigame", "" } },
            { 5, new List<string>() { "dialog", "after_minigame" } },
            { 6, new List<string>() { "game", "300" } },
            { 7, new List<string>() { "dialog", "home_again" } },
            { 8, new List<string>() { "dialog", "lesson" } },
            { 9, new List<string>() { "game", "-1" } } };

public static int money;

    public static int Money {
        get { return money; }
        set { money = value; }
    }

    public static int currentAct = 0;

    public static int CurrentAct
    {
        get { return currentAct; }
        set { currentAct = value; }
    }

    public static int currentDestination = -1;

    public static int CurrentDestination
    {
        get { return currentDestination; }
        set { currentDestination = value; }
    }

    public static string currentDialog;

    public static string CurrentDialog
    {
        get { return currentDialog; }
        set { currentDialog = value; }
    }
}
