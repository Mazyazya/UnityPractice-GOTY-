using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSpawner : MonoBehaviour
{
    public GameObject pref;

    void Start()
    {
        Script.OnMinigameStart += OnMinigameStart;
    }

    private void OnMinigameStart()
    {
        Instantiate(pref, new Vector3(0, -10, 0), Quaternion.identity);
    }

    void OnDestroy()
    {
        Script.OnMinigameStart -= OnMinigameStart;
    }
}
