using Assets.Scripts.NPC;
using Assets.Scripts.ObjectBehavior.Interactions;
using UnityEngine;

namespace Assets.Scripts.ActionItems
{
    public class NPC : Interactable
    {

        //public string[] dialogue;
        //public string name;
        // public Sprite NPCImage;
        private DialogueSystema _dialogue;

        private void Start()
        {
            _dialogue = GetComponent<DialogueSystema>();
        }

        public override void Interact()
        {
            // DialogueSystem.Instance.AddNewDialogue(dialogue,name,NPCImage);
            _dialogue.ShowDialogue = true;
            Debug.Log("Interacting with NPC 2");
        }
	
    }
}
