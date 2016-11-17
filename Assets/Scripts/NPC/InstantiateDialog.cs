using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstantiateDialog : MonoBehaviour {

    public TextAsset ta;
    public Dialog dialog;
    public int currentNode;
    public bool ShowDialogue;

    public GUISkin skin;

    public List<Answers> answers = new List<Answers>();

    void Start()
    {
        dialog = Dialog.Load(ta);
        skin = Resources.Load("Skin") as GUISkin;
    }

    void Update()
    {
        UpdateAnswers();
    }

    void UpdateAnswers()
    {
        answers.Clear();
        for (int i = 0; i < dialog.nodes[currentNode].answers.Length; i++)
        {
            if (dialog.nodes[currentNode].answers[i].QuestName == null || dialog.nodes[currentNode].answers[i].NeedQuestValue == PlayerPrefs.GetInt(dialog.nodes[currentNode].answers[i].QuestName))
                answers.Add(dialog.nodes[currentNode].answers[i]);
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (ShowDialogue)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 280, 500, 90), dialog.nodes[currentNode].NpcText);
            for (int i = 0; i < answers.Count; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 200 + 25 * i, 500, 25), answers[i].text, skin.label))
                {
                    if (answers[i].QuestValue > 0)
                    {
                        PlayerPrefs.SetInt(answers[i].QuestName, answers[i].QuestValue);
                    }
                    if (answers[i].end == "true")
                    {
                        ShowDialogue = false;
                    }
                    if (answers[i].RewardGold > 0)
                    {
                       // Character.Gold += answers[i].RewardGold;
                    }
                    currentNode = answers[i].nextNode;
                }
            }
        }
    }
}
