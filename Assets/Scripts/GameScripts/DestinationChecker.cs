using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationChecker : MonoBehaviour
{
    private int destination;
    void Start()
    {
        destination = StaticData.CurrentDestination;
        if (destination != -1)
        {
            MinigameScript.onMoneyChanged += onMoneyChanged;
        }
    }

    private void onMoneyChanged(int money)
    {
        if (money >= destination) {
            Debug.Log("Game complited");
            Script.NextAct();
        }
    }

    private void OnDestroy()
    {
        MinigameScript.onMoneyChanged -= onMoneyChanged;
    }
}
