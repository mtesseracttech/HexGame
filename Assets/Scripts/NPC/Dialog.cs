using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("dialogue")]
public class Dialog
{
    [XmlElement("node")]
    public Nodes[] nodes;

    public static Dialog Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
        StringReader reader = new StringReader(_xml.text);
        Dialog dial = serializer.Deserialize(reader) as Dialog;
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
    public Answers[] answers;
}

[System.Serializable]
public class Answers
{
    [XmlAttribute("tonode")]
    public int nextNode;
    [XmlElement("text")]
    public string text;
    [XmlElement("dialend")]
    public string end;


    [XmlAttribute("questvalue")]
    public int QuestValue;
    [XmlAttribute("needquestvalue")]
    public int NeedQuestValue;
    [XmlElement("questname")]
    public string QuestName;


    [XmlAttribute("rewardgold")]
    public int RewardGold;
}