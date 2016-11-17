using UnityEngine;
using System.Collections;

public class ActionItem : Interactable
{

    public string[] dialogue;
	//performs a damn action
    public override void Interact()
    {
       // DialogueSystem.Instance.AddNewDialogue(dialogue,"awdw");
        Debug.Log("Interacting base action Item");
    }
}
