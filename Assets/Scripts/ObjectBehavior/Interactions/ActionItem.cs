using UnityEngine;

namespace Assets.Scripts.ObjectBehavior.Interactions
{
    public class ActionItem : Interactable
    {

        public string[] Dialogue;
        //performs a damn action
        public override void Interact()
        {
            // DialogueSystem.Instance.AddNewDialogue(dialogue,"awdw");
            Debug.Log("Interacting base action Item");
        }
    }
}
