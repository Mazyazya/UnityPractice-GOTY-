using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public string dialogueText;      // ���� ��� ������ �������

    private DialogRoot dialogRoot;
    private int currentSceneIndex = 0; // ������� �����
    private int currentLineIndex = 0;  // ������� ������

    void Start()
    {
        LoadDialogues();
        DisplayNextLine();
    }

    // �������� JSON-�����
    private void LoadDialogues()
    {
        string jsn = Resources.Load<UnityEngine.TextAsset>("dialogues").text;
        dialogRoot = JsonUtility.FromJson<DialogRoot>(jsn);    
    }

    // ����� ��������� ������ �������
    public void DisplayNextLine()
    {
        if (dialogRoot == null || dialogRoot.dialogs.Count == 0)
        {
            Debug.LogError("������� �� ��������� ��� �����.");
            return;
        }

        if (currentSceneIndex < dialogRoot.dialogs.Count)
        {
            var currentScene = dialogRoot.dialogs[currentSceneIndex];
            background.GetComponent<Image>().sprite = Resources.Load<Sprite>(currentScene.background);
            if (currentLineIndex < currentScene.lines.Count)
            {
                var line = currentScene.lines[currentLineIndex];
                SettingDialog(line.character);
                dialogueText = line.text;
                currentLineIndex++;
               
                textComponent.text = "";

                StartTextAnimation(dialogueText);
            }
            else
            {
                currentSceneIndex++;
                currentLineIndex = 0;
                DisplayNextLine();
            }
        }
        else
        {
            Debug.Log("��� ������� ���������.");
        }
    }

    private void SettingDialog(string character)
    {
        if (character == "����")
        {
            characterNameText2.gameObject.SetActive(false);
            characterNameText1.gameObject.SetActive(true);
            character1.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
            character2.GetComponent<SpriteRenderer>().material.color = new Color(0.6f, 0.6f, 0.6f, 1);
            characterNameText1.text = character;
        }
        else
        {
            characterNameText1.gameObject.SetActive(false);
            characterNameText2.gameObject.SetActive(true);
            character1.GetComponent<SpriteRenderer>().material.color = new Color(0.6f, 0.6f, 0.6f, 1);
            character2.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
            characterNameText2.text = character;
        }
    }
    private Coroutine typingCoroutine;

    public void StartTextAnimation(string text)
    {
        // ���� �������� ��� ���, ������������� �
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // ��������� ����� ��������
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        textComponent.text = ""; // ������� ����� ����� ������� ��������

        foreach (char c in text)
        {
            textComponent.text += c; // ��������� ��������� ������
            yield return new WaitForSeconds(0.03f); // ��� �������� �����
        }

        typingCoroutine = null; // �������� ���������
    }
    private IEnumerator SlowScale(int h)
    {
        for (float q = 1f; q < 2f; q += .1f)
        {
            transform.localScale = new Vector2(h, h);
            yield return new WaitForSeconds(0.05f);
        }
    }
}