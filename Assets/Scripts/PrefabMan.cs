using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMan : MonoBehaviour
{
    public GameObject prefab;
    public int money;

    public void DistroyObj(string s)
    {
        Destroy(prefab);
    }
}
