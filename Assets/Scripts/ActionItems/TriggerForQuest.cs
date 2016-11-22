using Assets.Scripts.ObjectBehavior.Interactions;
using UnityEngine;

namespace Assets.Scripts.ActionItems
{
    public class TriggerForQuest : Interactable
    {
        public string QuestName ;
        [SerializeField]
        public int QuestAmount ;


        public override void Interact()
        {
            Debug.Log("Triggered the trig");
            if (PlayerPrefs.GetInt(QuestName) > 0)
            {
                Debug.Log("it was triggered");
                PlayerPrefs.SetInt(QuestName,QuestAmount);
            }
        }
    }
}
