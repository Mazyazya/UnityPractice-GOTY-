using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void StartGame()
    {
        StaticData.money = 0;
        StaticData.currentAct = 0;
        Script.StartGame();
    }
    public void ContinueGame()
    {
        Script.StartGame();
    }
}
