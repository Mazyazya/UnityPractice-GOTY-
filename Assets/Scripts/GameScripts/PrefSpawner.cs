using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;  // ���������� ��� ������ � ����������
    public GameObject canvas;
    public int gridWidth = 10;
    public int gridHeight = 4;
    public float tileSize = 1.0f;
    private float timer;

    void Start()
    {
        timer = Random.Range(5.0f, 20.0f);
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = Random.Range(5.0f, 10.0f);
            SpawnPrefabInRandomArea();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void SpawnPrefabInRandomArea()
    {
        // ����� ��������� �������
        int x = Random.Range(-gridWidth, gridWidth);
        int y = Random.Range(-gridHeight, gridHeight);

        // �������������� ��������� ����� � ������� ����������
        Vector3 worldPosition = new Vector3(x * tileSize, y * tileSize, 0);

        // �������� ���������� �������
        var obj = Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
        obj.transform.SetParent(canvas.transform, false);
    }
}
