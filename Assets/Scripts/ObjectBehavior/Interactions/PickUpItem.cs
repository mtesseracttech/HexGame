using UnityEngine;
using System.Collections;

public class PickUpItem : Interactable {

    public override void Interact()
    {
        Debug.Log("Interacting with item");
    }
}
