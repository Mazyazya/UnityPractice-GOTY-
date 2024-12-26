using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.EventSystems;

public class KakaScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject pref;
    [SerializeField] private GameObject obj1;
    private GameObject obj;
    private bool isObjectSelected;

    void Start() => MinigameScript.onCompliete += onComplite;
    void OnDestroy() => MinigameScript.onCompliete -= onComplite;

    private void onComplite()
    {
        if (isObjectSelected)
            Destroy(obj1);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isObjectSelected)
        {
            isObjectSelected = true;
            Debug.Log("Game Object Clicked!");
            obj = Instantiate(pref, new Vector3(0, -10, 0), Quaternion.identity);
        }
    }
}
