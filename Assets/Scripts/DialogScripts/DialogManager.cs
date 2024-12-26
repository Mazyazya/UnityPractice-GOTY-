using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textComponent;
    public Text characterNameText1; // Поле для имени персонажа
    public Text characterNameText2; // Поле для имени персонажа 2
    public GameObject character1;
    public GameObject character2;
    public GameObject background;
    public GameObject dialogMenu;
    public GameObject logicManager;
    public Button btn;

    public string dialogueText;      // Поле для текста реплики
    private Coroutine typingCoroutine;
    private bool isCoroutineStarts = false;

    private DialogRoot dialogRoot;
    private DialogScene currentScene;
    private int currentSceneIndex = 0; // Текущая сцена
    private int currentLineIndex = 0;  // Текущая строка

    private void Start()
    {   
        LoadDialogues();
        DisplayScene(StaticData.CurrentDialog);
    }

    // Загрузка JSON-файла
    public void LoadDialogues()
    {
        string jsn = Resources.Load<UnityEngine.TextAsset>("dialogues").text;
        dialogRoot = JsonUtility.FromJson<DialogRoot>(jsn);
        if (dialogRoot == null || dialogRoot.dialogs.Count == 0)
        {
            Debug.LogError("Диалоги не загружены или пусты.");
            return;
        }
    }

    public void DisplayScene(string currentDialog)
    {
        currentScene = FindScene(currentDialog);
        currentLineIndex = 0;
        currentSceneIndex++;
        SettingDialogeMenu(currentScene);
        DisplayNextLine(currentScene);
    }

    private DialogScene FindScene(string currendDialog)
    {
        foreach(var scene in dialogRoot.dialogs)
        {
            if (scene.scene == currendDialog)
            {
                return scene;
            }
        }
        Debug.Log("Ничего не найдено");
        return null;
    }
    public void Click()
    {
        if (isCoroutineStarts) //Если при нажатии работает корутин, он автоматически завершится и выведется полный текст
        {
            isCoroutineStarts = false;
        }       
        else
        {
            DisplayNextLine(currentScene);
        }
    }

    // Показ следующей строки диалога
    private void DisplayNextLine(DialogScene currentScene)
    {
        if (currentLineIndex < currentScene.lines.Count)
        {
            var line = currentScene.lines[currentLineIndex];
            currentLineIndex++;
            SettingDialog(line);
            dialogueText = line.text;
            textComponent.text = "";

            typingCoroutine = StartCoroutine(TypeText(dialogueText)); //Запуск вывода текста
        }
        else
        {
            btn.enabled = false;
            Script.NextAct();
        }
    }
    private void SettingDialogeMenu(DialogScene currentscene)
    {
        btn.enabled = true;
        background.GetComponent<Image>().sprite = Resources.Load<Sprite>(currentScene.background);
        character2.GetComponent<SpriteRenderer>().enabled = true;
        if (currentScene.lines[0].character == "Глеб")
        {
            try
            {
                character2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(currentScene.lines[1].icon);
            }
            catch {
                character2.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            character2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(currentScene.lines[0].icon);
        }
    }
    private void SettingDialog(DialogLine line)
    {   
        if (line.character == "Глеб")
        {
            characterNameText2.gameObject.SetActive(false);
            characterNameText1.gameObject.SetActive(true);
            character1.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
            character2.GetComponent<SpriteRenderer>().material.color = new Color(0.6f, 0.6f, 0.6f, 1);
            characterNameText1.text = line.character;
        }
        else
        {
            characterNameText1.gameObject.SetActive(false);
            characterNameText2.gameObject.SetActive(true);
            character2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(line.icon);
            character1.GetComponent<SpriteRenderer>().material.color = new Color(0.6f, 0.6f, 0.6f, 1);
            character2.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
            characterNameText2.text = line.character;
        }
    }

    private IEnumerator TypeText(string text)
    {
        isCoroutineStarts = true;
        textComponent.text = ""; // Очищаем текст перед началом анимации

        foreach (char c in text)
        {
            if (!isCoroutineStarts) //Вывод текста полностью при повторном нажатии при выполнении корутина
            {
                textComponent.text = text; 
                break;
            }
            textComponent.text += c; // Добавляем следующий символ
            yield return new WaitForSeconds(0.03f); // Ждём заданное время
        }

        isCoroutineStarts = false;
        typingCoroutine = null; // Анимация завершена
    }
}