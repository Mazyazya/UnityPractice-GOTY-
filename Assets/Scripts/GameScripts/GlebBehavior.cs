using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlebBehavior : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        GridSpawner.OnPrefSpawned += onPrefSpawned;
    }

    void OnDestroy()
    {
        GridSpawner.OnPrefSpawned -= onPrefSpawned;
    }

    private void onPrefSpawned()
    {
        m_Animator.Play("GlebTrigger");
    }

}
