using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstantiateDialog : MonoBehaviour {

    public TextAsset Ta;
    public int CurrentNode;
    public bool ShowDialogue;

    public GUISkin Skin;
    public List<Answers> answers = new List<Answers>();
    public Dialog dialog;
    void Start()
    {
        dialog = Dialog.Load(Ta);
        PlayerPrefs.DeleteAll();
        ShowDialogue = false;
    }

    void Update()
    {
        UpdateAnswers();
    }

    void UpdateAnswers()
    {
        answers.Clear();
       
        for (int i = 0; i < dialog.nodes[CurrentNode].answers.Length; i++)
        {
            if (dialog.nodes[CurrentNode].answers[i].QuestName == null ||
                dialog.nodes[CurrentNode].answers[i].NeedQuestValue ==
                PlayerPrefs.GetInt(dialog.nodes[CurrentNode].answers[i].QuestName))
            {
                answers.Add(dialog.nodes[CurrentNode].answers[i]);
            }
                
            
        }
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        if (ShowDialogue)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 280, 500, 90), dialog.nodes[CurrentNode].NpcText);
            for (int i = 0; i < answers.Count; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 200 + 25 * i, 500, 25), answers[i].text, Skin.label))
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
                    CurrentNode = answers[i].nextNode;
                  
                }
            }
        }
    }
}
