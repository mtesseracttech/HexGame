using System.Collections;
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
        
        [Header("Answer position")]
        public int PositionX;
        public int PositionY;

        void Start()
        {
            DialogXmlReader = DialogXmLreader.Load(Ta);
            PlayerPrefs.DeleteAll();
            //ShowDialogue = false;
            DialogueHud.SetActive(false);
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
               // NpCtext.text = DialogXmlReader.Nodes[CurrentNode].NpcText;
               StartCoroutine(TextType());

                NpcImage.sprite = NpcAvatar;
                Npcname.text = NameNpc;
                DialogueHud.SetActive(true);
            }
            else
            {
                DialogueHud.SetActive(false);
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
                    if (GUI.Button(new Rect(PositionX , Screen.height - PositionY + 40 * i, 400, 30), Answers[i].Text, Skin.label))
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

        IEnumerator TextType()
        {
            foreach (char letter in DialogXmlReader.Nodes[CurrentNode].NpcText)
            {
                NpCtext.text += letter;
                yield return new WaitForSeconds(0.9f);

            }
            
        }
    }
}
