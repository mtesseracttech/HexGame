using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.NPC
{
    public class DialogueSystema : MonoBehaviour {

        public TextAsset Ta;
        public int CurrentNode;
        public bool _showDialogue;
        private bool _endChat;

        public GUISkin Skin;
        
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

        public bool EndChat
        {
            set { _endChat = value; }
            get { return _endChat;}
        }

        public bool ShowDialogue
        {
            set { _showDialogue = value; }
            get { return _showDialogue; }
        }

        private void CreateDialogueCanvas()
        {
            if (_showDialogue)
            {
                NpCtext.text = DialogXmlReader.Nodes[CurrentNode].NpcText;
             //  StartCoroutine(TextType());

                NpcImage.sprite = NpcAvatar;
                Npcname.text = NameNpc;
                DialogueHud.SetActive(true);
                _endChat = false;
            }
            else
            {
                DialogueHud.SetActive(false);
            }
        }

        private void OnGUI()
        {
            GUI.skin = Skin;
            if (_showDialogue )//and animation is finished
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
                            _showDialogue = false;
                            DialogueHud.SetActive(false);
                            _endChat = true;
                            Debug.Log("we are done with dialogue");
                            //play animation of fading back

                        }
                        if (Answers[i].Win)
                        {
                            SceneManager.LoadScene(3);
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
