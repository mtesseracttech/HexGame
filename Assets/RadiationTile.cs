using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

public class RadiationTile : MonoBehaviour
{

    public     GameObject          Manager;
    public     GameObject          RadiationParticles;
    private    HexNodesManager     _manager;
    private    HexNode             _node;
    private    GameObject          _activeParticles;

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
            _activeParticles = Instantiate(RadiationParticles, transform.position, transform.rotation * Quaternion.Euler(-90,0,0));
            _activeParticles.transform.parent = this.transform;
        }
    }

    public void HideParticles()
    {
        _activeParticles = null;
    }
}
