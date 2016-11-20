/*using System;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine;

public class Node:Canvas, IComparable<Node>
{
	private Vec2 _position;
	private List<Node> _connections;
	private int _size;

	//additions specific for path finding
	private Node _nodeParent;									//keep track of where we came from
	public float costCurrent = 0;								//keep track of cost up to now
	public float costEstimate = 0;								//keep track of cost estimate to goal
    public float costCombined = 0;

	private Dictionary<Node, LineSegment> _nodeToLineSegment;	//keep track of linesegment corresponding with a specific parent
	private TextField _text;									//display debug info to user

	public Node (int pSize, Color pColor, Vec2 pPosition):base (pSize, pSize)
	{
		_size = pSize;
		SetOrigin (_size / 2, _size / 2);
		position = pPosition;
		SetColor (pColor);
		_connections = new List<Node> ();

		//added stuff for path finding:
		//to view/debug the link to the parent
		_nodeToLineSegment = new Dictionary<Node, LineSegment> ();				

		//to show text on the node
		_text = new TextField (_size-4, _size-4);
		_text.SetOrigin (_text.width/2, _text.height/2);
		_text.font = new Font ("Arial", 8,FontStyle.Bold);
		_text.textColor = Color.White;
		_text.text = "";
		AddChild (_text);
	}

	public void SetColor(Color pColor) {
		graphics.FillRectangle (new SolidBrush (pColor), 0, 0, _size, _size);
	}

	public Vec2 position {
		set {
			_position = value ?? Vec2.zero;
			x = _position.x;
			y = _position.y;
		}
		get {
			return _position;
		}
	}

	public Node parentNode {
		get { return _nodeParent; }
		set {
			//reset color to default
			if (_nodeParent != null) parentLink.color = (uint)Color.LightGray.ToArgb ();
			_nodeParent = value;

			//reset color to represent we have a parent and put the link on top of the display list so it doesn't cross behind other lines
			if (_nodeParent != null) {
				parentLink.color = (uint)Color.DarkSlateGray.ToArgb ();
				parentLink.parent.AddChild (parentLink);
			}
		}
	}

	//if parent node is set, this returns the corresponding linesegment
	public LineSegment parentLink {
		get { return _nodeToLineSegment [_nodeParent]; }
	}

	public void AddConnection (Node node, LineSegment visualLink) {
		_connections.Add (node);

		//for debugging, map node connection to a corresponding line segment
		_nodeToLineSegment [node] = visualLink;
	}

	public bool HasConnection (Node node) {
		return _connections.IndexOf (node) > -1;
	}

	public int GetConnectionCount() {
		return _connections.Count;
	}

	public Node GetConnectionAt (int index) {
		return _connections[index];
	}

	public override string ToString ()
	{
		return "Node ("+x+","+y+") with cost:" + costCurrent +"|"+ costEstimate+"|"+(costCurrent + costEstimate);
	}

	public string info {
		set {
			_text.text = value;
		}
	}

	#region IComparable implementation

	public int CompareTo (Node pOther)
	{
        //return costCurrent.CompareTo (pOther.costCurrent);
        //return costEstimate.CompareTo(pOther.costEstimate);

	    return (costCurrent + costEstimate).CompareTo(pOther.costCurrent + pOther.costEstimate);

	}
		
	#endregion

}

*/
