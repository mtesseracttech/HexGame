using UnityEngine;
using System.Collections;

public class SignPost : ActionItem {

    public override void Interact()
    {       
        base.Interact();
        Debug.Log("interacting with sign post");
    }
}
