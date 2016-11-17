using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
//holder of all our quests
    public delegate void ActorEventHandler(int ActorID);

    public static event ActorEventHandler OnActorEliminate;

    public Quest Quest01;

    private void Start()
    {
        Quest01 = new Quest()
        {
            Name = "Eliminate jack the wild and his army generals",
            Description = "Go to the Orc fortress and eliminate Jack the wild"
        };

        Quest01.AddTask(new EliminationTask()
        {
            Name = "Eliminate jack the wild",
            Description = "Eliminate jack the wild",
            ActorID = 0,
            isOptional = false,
            Quantity = 1
        });
        Quest01.AddTask(new EliminationTask()
        {
            Name = "Eliminate Army general",
            Description = "Eliminate Army general",
            ActorID = 1,
            isOptional = false,
            Quantity = 3
        });
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20,20,Screen.width-40,Screen.height-40));
        GUILayout.BeginVertical();
        Quest01.Display();
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Begin quest"))
        {
            if (Quest01.Status != Quest.QuestState.Complete)
                Quest01.Status = Quest.QuestState.Active;
        }
        if (GUILayout.Button("Eliminate Jack the Wild"))
        {
            if (OnActorEliminate != null)
                OnActorEliminate(0);
        }
        if (GUILayout.Button("Eliminate Army General"))
        {
            if (OnActorEliminate != null)
                OnActorEliminate(1);
        }
        if (GUILayout.Button("Complete Quest"))
        {
            if (Quest01.Status != Quest.QuestState.Pending)
                Quest01.Status = Quest.QuestState.Complete;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
}
