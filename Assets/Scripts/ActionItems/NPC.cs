using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPC : Interactable
{

    //public string[] dialogue;
    //public string name;
   // public Sprite NPCImage;
    public Dialogue dialogyue;

    public void Start()
    {
        dialogyue = GetComponent<Dialogue>();
    }

    public override void Interact()
    {
       // DialogueSystem.Instance.AddNewDialogue(dialogue,name,NPCImage);
        dialogyue.ShowDialogue = true;
        Debug.Log("Interacting with NPC 2");
    }
	
}
