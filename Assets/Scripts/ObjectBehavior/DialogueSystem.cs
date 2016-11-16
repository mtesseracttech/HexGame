using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour {
    [HideInInspector]
    public static DialogueSystem Instance { get; set; }
    [HideInInspector]
    public List<string> dialogueLines;
    [HideInInspector]
    public string NpcName;

    public GameObject dialoguePanel;

    private Button continueButton;
    private Text dialogueText, nameText;
    private int dialogueIndex;


    void Awake()
    {
        continueButton = dialoguePanel.transform.FindChild("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate {ContinueDialogue(); });
        dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
        nameText = dialoguePanel.transform.FindChild("Name").GetChild(0).GetComponent<Text>();
        
        dialoguePanel.SetActive(false);    

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines,string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        NpcName = npcName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = NpcName;
        dialoguePanel.SetActive(true);

    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count-1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }


}
