using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogLine
{
    public string icon;
    public string character; // ��� ���������
    public string text;      // ����� �������
}

[System.Serializable]
public class DialogScene
{
    public string scene;     // �������� �����
    public string sceneID;
    public string background;
    public List<DialogLine> lines; // ������ ����� �������
}

[System.Serializable]
public class DialogRoot
{
    public List<DialogScene> dialogs; // ��� ����� � ���������
}