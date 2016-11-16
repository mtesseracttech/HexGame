using UnityEngine;
using System.Collections;

public class WorldInteractions : MonoBehaviour
{
    private NavMeshAgent playerAgent;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //check if we are howering over the UI and going to stop there and not send an array to the world
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
    }


    //it's going to handle checking what are we going to interact with
    void GetInteraction()
    {
        Ray ineractionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(ineractionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactionObject = interactionInfo.collider.gameObject;
            if (interactionObject.tag == "Interactable object")
            {
                interactionObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else
            {
                playerAgent.stoppingDistance = 0;
                playerAgent.destination = interactionInfo.point;
            }
        }
    }
}
