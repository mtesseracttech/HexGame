using Assets.Scripts.ObjectBehavior.Interactions;
using UnityEngine;

namespace Assets.Scripts.ActionItems
{
    public class PickUpItem : Interactable {

        public override void Interact()
        {
            Debug.Log("Interacting with item");
        }
    }
}
