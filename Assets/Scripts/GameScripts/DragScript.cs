using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private float posX;
    private float posY;
    [SerializeField] private GameObject player;

    void Update()
    {
        if (Input.GetMouseButton(0)) //0 - ����� ������ ����, � �� ��������� ������� ����
        {
            posX = Input.mousePosition.x; //x
            posY = Input.mousePosition.y; //y

            player.transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(posX, posY, 0)); // ������������ �������� ���������� � �������
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0); //������� ���� ���������� z ��������� ��� -10, ������� ��� ��� �����
        }
    }
}
