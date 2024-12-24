using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject prefab;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private int money;
   
    private float x = 1;
    public delegate void OnMoneyChanged(int money);
    public static event OnMoneyChanged onMoneyChanged;
    private bool isComplieted;
    
    public static event Action onCompliete;

    private void Start()
    {
        menu.SetActive(true);
        m_Animator.Play("EnterAnim");
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        x -= 0.1f;
        Debug.Log("Касание триггера");
        if (x < 0.5 && !isComplieted) {
            obj.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        if (x < 0 && !isComplieted)
        {
            isComplieted = true;
            obj.GetComponent<SpriteRenderer>().sprite = sprites[2];
            obj.GetComponent<Collider2D>().enabled = false;
            GameComplieted();
        }
    }

    private void GameComplieted()
    {
        StaticData.money += money;
        onMoneyChanged?.Invoke(StaticData.money);
        onCompliete.Invoke();
        m_Animator.Play("ExitAnim");
    }
}
