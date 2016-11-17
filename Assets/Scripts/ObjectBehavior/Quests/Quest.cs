using UnityEngine;
using System.Collections.Generic;

public class Quest
{
    public enum QuestState
    {
        Inactive, Active,Partial,Pending, Complete
    }

    private QuestState _status;
    private string _name;
    private string _description;
    private List<Task> _task;

    public QuestState Status
    {
        get { return _status; }
        set { _status = value; }
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    public Quest()
    {
        _status = QuestState.Inactive;
        _name = string.Empty;
        _description = string.Empty;
        _task = new List<Task>();

        OnCreate();
    }

    public void AddTask(Task task)
    {
        if (task.Type == Task.TaskType.Collection)
        {
            Debug.Log("Collection task");
        }
        _task.Add(task);
    }

    public void Display()
    {
        GUILayout.Label(string.Format("Status:\t\t{0}", _status.ToString()));
        GUILayout.Label(string.Format("Name:\t\t{0}", _name));
        GUILayout.Label(string.Format("Description:\t\t{0}", _description));
        foreach (Task task in _task)
        {
            GUILayout.Space(40);
            task.Display();
        }
    }

    public void OnCreate()
    {
        Task.OnComplete += Task_OnComplete;
    }

    private void Task_OnComplete(Task task)
    {
        bool allComplete = true;

        foreach (Task t in _task)
        {
            if (t.State != Task.TaskState.Complete)
            {
                allComplete = false;
            }
            else
            {
                _status = QuestState.Partial;
            }
        }
        if(allComplete == true)
            _status = QuestState.Pending;
    }
}
