using UnityEngine;

namespace Assets.Scripts.ObjectBehavior.Interactions
{
    public class Interactable : MonoBehaviour
    {
        // protected NavMeshAgent playerAgent;
        // private bool hasInteracted;

        public virtual void MoveToInteraction(/*UnityEngine.AI*/UnityEngine.AI.NavMeshAgent pPlayerAgent)
        {
            // hasInteracted = false;
            //  playerAgent = pPlayerAgent;
            pPlayerAgent.stoppingDistance = 3f;
            pPlayerAgent.destination = transform.position;
        
            Interact();
        }

        void Update()
        {
            /*
         //if we haven't interacted yet and we are still not there we can interact
        if (playerAgent != null && playerAgent.pathPending && !hasInteracted)
        { //check the distance from player to object
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)//because i change the stop distance in some places
            {
                Interact();
                hasInteracted = true;
            }
        }
        **/

        }

        public virtual void Interact()
        {
            Debug.Log("Interacting with base class.");
        }


    }
}
