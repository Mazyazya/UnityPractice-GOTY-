using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;  // Установите ваш префаб в инспекторе
    public GameObject canvas;
    private float gridWidth = 10;
    private float gridHeight = 4;
    public float tileSize = 1.0f;
    private float timer;

    public static event Action OnPrefSpawned;

    void Start()
    {
        timer = UnityEngine.Random.Range(5.0f, 15.0f);
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = UnityEngine.Random.Range(5.0f, 10.0f);
            SpawnPrefabInRandomArea();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void SpawnPrefabInRandomArea()
    {
        // Выбор случайной позиции
        float x = UnityEngine.Random.Range(-9f, gridWidth);
        float y = UnityEngine.Random.Range(-gridHeight, gridHeight);

        // Преобразование координат сетки в мировые координаты
        Vector3 worldPosition = new Vector3(x * tileSize, y * tileSize, 0);

        // Создание экземпляра префаба
        var obj = Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
        obj.transform.SetParent(canvas.transform, false);

        OnPrefSpawned.Invoke();
    }
}
