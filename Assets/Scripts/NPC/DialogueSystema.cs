using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.NPC
{
    public class DialogueSystema : MonoBehaviour {

        public TextAsset Ta;
        public int CurrentNode;
        public bool ShowDialogue;

        public GUISkin Skin;
        [Header("Answers")]
        public List<PlayerAnswer> Answers = new List<PlayerAnswer>();
        public DialogXmLreader DialogXmlReader;

        [Header("HUD references")]
        public GameObject DialogueHud;
        public Image NpcImage;
        public Text NpCtext;
        public Text Npcname;

        [Header("NPC info")]
        public Sprite NpcAvatar;
        public string NameNpc;

        void Start()
        {
            DialogXmlReader = DialogXmLreader.Load(Ta);
            PlayerPrefs.DeleteAll();
            //ShowDialogue = false;
        }

        private void Update()
        {
            UpdateAnswers();
            CreateDialogueCanvas();
        }

        private void UpdateAnswers()
        {
            Answers.Clear();
       
            for (int i = 0; i < DialogXmlReader.Nodes[CurrentNode].answers.Length; i++)
            {
                if (DialogXmlReader.Nodes[CurrentNode].answers[i].QuestName == null ||
                    DialogXmlReader.Nodes[CurrentNode].answers[i].NeedQuestValue ==
                    PlayerPrefs.GetInt(DialogXmlReader.Nodes[CurrentNode].answers[i].QuestName))
                {
                    Answers.Add(DialogXmlReader.Nodes[CurrentNode].answers[i]);
                }
                
            
            }
        }

        private void CreateDialogueCanvas()
        {
            if (ShowDialogue)
            {
                NpCtext.text = DialogXmlReader.Nodes[CurrentNode].NpcText;
                NpcImage.sprite = NpcAvatar;
                Npcname.text = NameNpc;
            }
        }

        private void OnGUI()
        {
            GUI.skin = Skin;
            if (ShowDialogue)//and animation is finished
            {
                // GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
                // GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 280, 500, 90), DialogXmlReader.nodes[CurrentNode].NpcText);
                for (int i = 0; i < Answers.Count; i++)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 450, Screen.height - 150 + 25 * i, 500, 25), Answers[i].Text, Skin.label))
                    {
                        if (Answers[i].QuestValue > 0)
                        {
                            PlayerPrefs.SetInt(Answers[i].QuestName, Answers[i].QuestValue);
                        }
                        if (Answers[i].End == "true")
                        {
                            ShowDialogue = false;
                            DialogueHud.SetActive(false);
                            //play animation of fading back
                            
                        }
                        if (Answers[i].RewardGold > 0)
                        {
                            // Character.Gold += PlayerAnswer[i].RewardGold;
                        }
                        CurrentNode = Answers[i].NextNode;
                  
                    }
                }
            }
        }
    }
}
