using UnityEngine;
using System.Collections;

public class TriggerForQuest : Interactable
{
    
    public string QuestName = "Quest1";
    [SerializeField]
    public int QuestAmount = 2;


    public override void Interact()
    {
       Debug.Log("Triggered the trig");
        if (PlayerPrefs.GetInt("Quest1") >= 0)
        {
            Debug.Log("it was triggered");
             PlayerPrefs.SetInt(QuestName,QuestAmount);
        }
    }
}
