using UnityEngine;
using System.Collections;

public class TriggerForQuest : Interactable
{

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            Debug.Log("it was triggeredsefsefhjue");
            if (PlayerPrefs.GetInt("Quest1") == 0)
            {
                Debug.Log("it was triggered");
               // PlayerPrefs.SetInt("Quest1",2);
            }
        }   
    }


    public override void Interact()
    {
       Debug.Log("Triggered the trig");
        if (PlayerPrefs.GetInt("Quest1") >= 0)
        {
            Debug.Log("it was triggered");
             PlayerPrefs.SetInt("Quest1",2);
        }
    }
}
