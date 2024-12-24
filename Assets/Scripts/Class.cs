using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogLine
{
    public string icon;
    public string character; // Имя персонажа
    public string text;      // Текст реплики
}

[System.Serializable]
public class DialogScene
{
    public string scene;     // Название сцены
    public string sceneID;
    public string background;
    public List<DialogLine> lines; // Список строк диалога
}

[System.Serializable]
public class DialogRoot
{
    public List<DialogScene> dialogs; // Все сцены с диалогами
}