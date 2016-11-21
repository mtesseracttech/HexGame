﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPC : Interactable
{

    //public string[] dialogue;
    //public string name;
   // public Sprite NPCImage;
    private InstantiateDialog Dialogyue;

    public void Start()
    {
        Dialogyue = GetComponent<InstantiateDialog>();
    }

    public override void Interact()
    {
       // DialogueSystem.Instance.AddNewDialogue(dialogue,name,NPCImage);
        Dialogyue.ShowDialogue = true;
        Debug.Log("Interacting with NPC 2");
    }
	
}
