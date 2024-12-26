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
    public Text characterNameText1; // ���� ��� ����� ���������
    public Text characterNameText2; // ���� ��� ����� ��������� 2
    public GameObject character1;
    public GameObject character2;
    public GameObject background;
    public GameObject dialogMenu;
    public GameObject logicManager;
    public Button btn;

    public string dialogueText;      // ���� ��� ������ �������
    private Coroutine typingCoroutine;
    private bool isCoroutineStarts = false;

    private DialogRoot dialogRoot;
    private DialogScene currentScene;
    private int currentSceneIndex = 0; // ������� �����
    private int currentLineIndex = 0;  // ������� ������

    private void Start()
    {   
        LoadDialogues();
        DisplayScene(StaticData.CurrentDialog);
    }

    // �������� JSON-�����
    public void LoadDialogues()
    {
        string jsn = Resources.Load<UnityEngine.TextAsset>("dialogues").text;
        dialogRoot = JsonUtility.FromJson<DialogRoot>(jsn);
        if (dialogRoot == null || dialogRoot.dialogs.Count == 0)
        {
            Debug.LogError("������� �� ��������� ��� �����.");
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
        Debug.Log("������ �� �������");
        return null;
    }
    public void Click()
    {
        if (isCoroutineStarts) //���� ��� ������� �������� �������, �� ������������� ���������� � ��������� ������ �����
        {
            isCoroutineStarts = false;
        }       
        else
        {
            DisplayNextLine(currentScene);
        }
    }

    // ����� ��������� ������ �������
    private void DisplayNextLine(DialogScene currentScene)
    {
        if (currentLineIndex < currentScene.lines.Count)
        {
            var line = currentScene.lines[currentLineIndex];
            currentLineIndex++;
            SettingDialog(line);
            dialogueText = line.text;
            textComponent.text = "";

            typingCoroutine = StartCoroutine(TypeText(dialogueText)); //������ ������ ������
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
        if (currentScene.lines[0].character == "����")
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
        if (line.character == "����")
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
        textComponent.text = ""; // ������� ����� ����� ������� ��������

        foreach (char c in text)
        {
            if (!isCoroutineStarts) //����� ������ ��������� ��� ��������� ������� ��� ���������� ��������
            {
                textComponent.text = text; 
                break;
            }
            textComponent.text += c; // ��������� ��������� ������
            yield return new WaitForSeconds(0.03f); // ��� �������� �����
        }

        isCoroutineStarts = false;
        typingCoroutine = null; // �������� ���������
    }
}