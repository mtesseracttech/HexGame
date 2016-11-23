using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialoguePanel;
    public static DialogueSystem Instance { get; set; }
    private List<string> DialogueLines = new List<string>();

    private string npcName;
    private Sprite NpcImage;

    public Text dialogueText, nameText;
    private int dialogueIndex;
    public Image imageNPC;
    public Button continueButton;

    void Awake()
    {
        //dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else Instance = this;
    }

    public void AddNewDialogue(string[] pLines, string pName, Sprite pNpCimage)
    {
        dialogueIndex = 0;
        DialogueLines = new List<string>(pLines.Length);
        DialogueLines.AddRange(pLines);
        npcName = pName;
        NpcImage = pNpCimage;

        CreateDialogue();
       
    }
    public void CreateDialogue()
    {
        dialogueText.text = DialogueLines[dialogueIndex];
        nameText.text = npcName;
        imageNPC.sprite = NpcImage;
        dialoguePanel.SetActive(true);
        
        //set image

    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < DialogueLines.Count-1)
        {
            dialogueIndex++;
            dialogueText.text = DialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
