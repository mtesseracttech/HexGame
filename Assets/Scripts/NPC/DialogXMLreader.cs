using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    [XmlRoot("dialogue")]
    public class DialogXmLreader
    {
        [XmlElement("node")]
        public Nodes[] Nodes;

        public static DialogXmLreader Load(TextAsset xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DialogXmLreader));
            StringReader reader = new StringReader(xml.text);
            DialogXmLreader dial = serializer.Deserialize(reader) as DialogXmLreader;
            return dial;
        }
    }

    [System.Serializable]
    public class Nodes
    {
        [XmlElement("npctext")]
        public string NpcText;

        [XmlArray("answers")]
        [XmlArrayItem("answer")]
        public PlayerAnswer[] answers;
    }

    [System.Serializable]
    public class PlayerAnswer
    {
        [XmlAttribute("tonode")]
        public int NextNode;
        [XmlElement("text")]
        public string Text;
        [XmlElement("dialend")]
        public string End;


        [XmlAttribute("questvalue")]
        public int QuestValue;
        [XmlAttribute("needquestvalue")]
        public int NeedQuestValue;
        [XmlElement("questname")]
        public string QuestName;


        [XmlAttribute("rewardgold")]
        public int RewardGold;
    }
}