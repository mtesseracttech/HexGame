using UnityEngine;
using System.Collections;
using Assets.Scripts.AI;
using System.Collections.Generic;

public class BreadthFirst : MonoBehaviour {

	private Queue <HexNode> _frontier;
	private HashSet <HexNode> _cameFrom;
	[SerializeField]
	int _maxExpansion;

	public void Search (HexNode root) {
		root.Expansion = 0;
		_frontier = new Queue<HexNode> ();
		_cameFrom = new HashSet<HexNode> ();

		_frontier.Enqueue (root);
		_cameFrom.Add (root);

		while (_frontier.Count > 0) {
			var current = _frontier.Dequeue ();

			foreach (var next in current.Neighbors) {				

				if (next.Expansion > _maxExpansion) {
					continue;
				} else {
					next.Expansion = current.Expansion + 1;
				}

				if (!_cameFrom.Contains (next)) {
					_frontier.Enqueue (next);
					_cameFrom.Add (next);

					//Add the walkable tile indication here
				}
			}
		}
	}
}
