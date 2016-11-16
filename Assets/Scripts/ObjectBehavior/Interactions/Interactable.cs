using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    protected NavMeshAgent playerAgent;
    private bool hasInteracted;

    public virtual void MoveToInteraction(NavMeshAgent pPlayerAgent)
    {
        hasInteracted = false;
        playerAgent = pPlayerAgent;
        pPlayerAgent.stoppingDistance = 3f;
        pPlayerAgent.destination = transform.position;

        Interact();
    }

    void Update()
    {
        if (playerAgent != null && !playerAgent.pathPending && !hasInteracted)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)//because i change the stop distance in some places
            {
                Interact();
                hasInteracted = true;
            }
        }

    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }


}
