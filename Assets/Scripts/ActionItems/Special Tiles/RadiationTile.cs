using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

public class RadiationTile : MonoBehaviour
{

    public     GameObject          Manager;
    public     GameObject          RadiationParticles;
    private    HexNodesManager     _manager;
    private    HexNode             _node;
    private    GameObject          _activeParticles;
    private    Vector3             _offset             = new Vector3(0, 0.6f, 0);
    private void Start ()
	{
	    _manager           = Manager.GetComponent<HexNodesManager>();
	    _node              = _manager.ReturnClosestHexNode(transform.position);
	    transform.position = _node.Position;
	    _manager.RegisterRadationTile(this);
	    ShowParticles();
	}

    public void ShowParticles()
    {
        if (_activeParticles == null)
        {
            _activeParticles = Instantiate(RadiationParticles, transform.position + _offset, transform.rotation * Quaternion.Euler(-90,0,0));
            _activeParticles.transform.parent = transform;
        }
    }

    public void HideParticles()
    {
        _activeParticles = null;
    }

    public HexNode GetNode()
    {
        return _node;
    }
}
