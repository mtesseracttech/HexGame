using Assets.Scripts.ObjectBehavior.Interactions;
using UnityEngine;

namespace Assets.Scripts.ActionItems
{
    public class SignPost : ActionItem {

        public override void Interact()
        {       
            base.Interact();
            Debug.Log("interacting with sign post");
        }
    }
}
