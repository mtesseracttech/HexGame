using Assets.Scripts.AI.Pathfinding;
using Assets.Scripts.Inventory;
using UnityEngine;

public class BluePill : Items
{

    private HexNode _targetNode;
    public Inventory Inventory;

    public override void Start()
    {
        base.Start();
        Position = CurrentNode.Position;
    }

    public override void Interact()
    {
        Inventory.AddItem(5);
        Destroy(gameObject);
    }


    public HexNode CurrentNode
    {
        get { return GetCurrentNode(); }
        set
        {
            SetCurrentNode(value);
            Position = CurrentNode.Position;
        }
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Quaternion Rotation
    {
        get { return transform.rotation; }
        set { transform.rotation = value; }
    }
}
