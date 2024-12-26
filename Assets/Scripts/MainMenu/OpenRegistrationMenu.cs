using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenRegistrationMenu : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_Canvas;
    [SerializeField] private InputField inputField;
    public void OpenMenu()
    {
        m_Canvas.SetActive(true);
        m_Animator.Play("EnterAnim");
        inputField.text = DataBaseManager.GetPlayersName();
    }

    public void ÑloseMenu()
    {
        m_Animator.Play("ExitAnim");
        DataBaseManager.CreateNewPlayer(inputField.text);
    }

    public void DisactiveMenu()
    {
        m_Canvas.SetActive(false);
    }
}
