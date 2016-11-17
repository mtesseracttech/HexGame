using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public abstract class Task
{

    public delegate void TaskEventHandler(Task task);
    public static event TaskEventHandler OnComplete;

    public enum TaskState {Active,Inactive ,Complete}
    public enum TaskType { Collection, Delivery, Elimination, Interaction}

    private TaskType _type;
    private TaskState _status;
    private string _name;
    private string _description;
    private bool _optional;

    public TaskType Type
    {
        get { return _type;}
        set { _type = value; }
    }
    public TaskState State
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
    public bool isOptional
    {
        get { return _optional; }
        set { _optional = value; }
    }

    public Task()
    {
        _type = TaskType.Collection;
        _status = TaskState.Inactive;
        _name = string.Empty;
        _description = string.Empty;
        _optional = false;
        OnCreate();
    }

    public virtual void Display()
    {
        GUILayout.Label(string.Format("Type:\t\t{0}", _type.ToString()));
        GUILayout.Label(string.Format("Status:\t\t{0}", _status.ToString()));
        GUILayout.Label(string.Format("Name:\t\t{0}", _name));
        GUILayout.Label(string.Format("Description:\t\t{0}", _description));
        GUILayout.Label(string.Format("Optional:\t\t{0}", _optional));
    }

    public virtual void CompleteTask()
    {
        _status = TaskState.Complete;
        if (OnComplete != null)
            OnComplete(this);
    }

    public abstract void OnCreate();
}

public class CollectionTask : Task
{
    private string _itemName;
    private int _quantity;

    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }


    public CollectionTask()
    {
        Type = TaskType.Collection;
        _itemName = string.Empty;
        _quantity = 0;
    }

    public override void Display()
    {
        base.Display();
        GUILayout.Label(string.Format("Item name:\t\t{0}", _itemName));
        GUILayout.Label(string.Format("Quantity:\t\t{0}", _quantity));
    }
    public override void OnCreate()
    {}
}

public class DeliveryTask : Task
{
    private string _itemName;
    private int _quantity;
    private int _actorID;

    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public int ActorID
    {
        get { return _actorID; }
        set { _actorID = value; }
    }
    public DeliveryTask()
    {
        Type = TaskType.Delivery;
        _itemName = string.Empty;
        _quantity = 0;
        _actorID = 0;
    }

    public override void Display()
    {
        base.Display();
        GUILayout.Label(string.Format("Item name:\t\t{0}", _itemName));
        GUILayout.Label(string.Format("Quantity:\t\t{0}", _quantity));
        GUILayout.Label(string.Format("ActorID:\t\t{0}", _actorID));
    }
    public override void OnCreate()
    { }
}

public class EliminationTask : Task{

    private int _quantity;
    private int _actorID;

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public int ActorID
    {
        get { return _actorID; }
        set { _actorID = value; }
    }

    public EliminationTask()
    {
        Type = TaskType.Elimination;
        _quantity = 0;
        _actorID = 0;

    }
    public override void Display()
    {
        base.Display();
        GUILayout.Label(string.Format("Quantity:\t\t{0}", _quantity));
        GUILayout.Label(string.Format("ActorID:\t\t{0}", _actorID));
    }
    public override void OnCreate()
    { }
}

public class InteractionTask : Task{

    private int _actorID;

    public int ActorID
    {
        get { return _actorID; }
        set { _actorID = value; }
    }

    public InteractionTask()
    {
        Type = TaskType.Interaction;
        _actorID = 0;
    }
    public override void Display()
    {
        base.Display();
        GUILayout.Label(string.Format("ActorID:\t\t{0}", _actorID));
    }
    public override void OnCreate()
    { }
}

