using UnityEngine;

namespace Assets.Scripts.ActionItems
{
    public class TriggerForQuest : MonoBehaviour
    {
        public string QuestName ;
        public int QuestAmount ;
        public int QuestAmountCheck;

        public void Interact()
        {
            Debug.Log("Triggered the trig");
            if (PlayerPrefs.GetInt(QuestName) > QuestAmountCheck)
            {
                Debug.Log("it was triggered");
                PlayerPrefs.SetInt(QuestName,QuestAmount);
            }
        }
    }
}
