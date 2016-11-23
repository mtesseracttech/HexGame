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
        
        [Header("NPC name position")]
        private int NpcNameX = 20;
        private int NpcNameY = 785;

        [Header("NPC text position")]
        private int NpcTextX = 150;
        private int NpcTextY = 560;

        [Header("NPC box position")]
        private int BoxPositionX = -5;
        private int BoxPositionY = 530;

        [Header("BOX size")]
        private int BoxWidth = 700;
        private int BoxHeight = 300;

        [Header("Answer position")]
        public int PositionX;
        public int PositionY;

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
               //  GUI.Box(new Rect(BoxPositionX,BoxPositionY, BoxWidth, BoxHeight), "");
                // GUI.Label(new Rect(NpcTextX, NpcTextY, 500, 90), DialogXmlReader.Nodes[CurrentNode].NpcText);
                // GUI.Label(new Rect(NpcNameX,NpcNameY,200,100),NameNpc);
                

                for (int i = 0; i < Answers.Count; i++)
                {
                    if (GUI.Button(new Rect(PositionX , Screen.height - PositionY + 25 * i, 500, 25), Answers[i].Text, Skin.label))
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
