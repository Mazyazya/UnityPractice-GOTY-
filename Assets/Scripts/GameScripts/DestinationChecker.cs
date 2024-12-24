using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MinigameScript.onMoneyChanged += onMoneyChanged;
    }

    private void onMoneyChanged(int money)
    {
        if (money >= 300) {
            Debug.Log("Game complited");
            SceneManager.LoadScene("Dialogs");
        }
    }
}
