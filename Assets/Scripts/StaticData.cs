using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static int money;

    public static int Money {
        get { return money; }
        set { money = value; }
    }

    public static int currentAct = 1;

    public static int CurrentAct
    {
        get { return money; }
        set { money = value; }
    }
}
