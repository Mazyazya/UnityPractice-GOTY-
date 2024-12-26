using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{

    [SerializeField] private GameObject obj;
    [SerializeField] private Button continueButton;
   
    private void Start()
    {
        DataBaseManager.currentPlayerId = PlayerPrefs.GetInt("savedId");
        if (DataBaseManager.currentPlayerId != 0)
            DataBaseManager.LoadData();
        else
        {
           obj.GetComponent<OpenRegistrationMenu>().OpenMenu();
        }
    }
    private void Update()
    {
        if (StaticData.currentAct != 0)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }
}
