using UnityEngine;
using System.Collections;
using Assets.Scripts.AI;
using System.Collections.Generic;

public class BreadthFirst : MonoBehaviour {

	private Queue <HexNode> _frontier;
	private HashSet <HexNode> _cameFrom;
	[SerializeField]
	int _maxExpansion;
	[SerializeField]
	GameObject _walkHighlightPrefab;
	[SerializeField]
	GameObject _attackHighlightPrefab;
	[SerializeField]
	GameObject _terrainHighlightPrefab;
	private List <GameObject> _highlights = new List<GameObject> ();

	public IEnumerator Search (HexNode root) {
		ClearHighlights ();
		root.Expansion = 0;

		if (root.HasBuilding == true) {
			
		} else {

			_frontier = new Queue<HexNode> ();
			_cameFrom = new HashSet<HexNode> ();

			_frontier.Enqueue (root);
			_cameFrom.Add (root);

			while (_frontier.Count > 0) {
				var current = _frontier.Dequeue ();


				foreach (var next in current.Neighbors) {	

					if (_cameFrom.Contains (next) || next.HasBuilding == true || next.HasEnemy == true) {
						if (next.HasBuilding == true && !_cameFrom.Contains (next) && current.Expansion < _maxExpansion) {
							GameObject terrainHighlight = (GameObject)Instantiate (_terrainHighlightPrefab, next.Position, Quaternion.AngleAxis (-90, new Vector3 (1, 0, 0)));
							terrainHighlight.SendMessage("Create", next);
						    _highlights.Add (terrainHighlight);
							_cameFrom.Add (next);
						}

						if (next.HasEnemy == true && !_cameFrom.Contains (next) && current.Expansion < _maxExpansion) {
							GameObject enemyHighlight = (GameObject)Instantiate (_attackHighlightPrefab, next.Position, Quaternion.AngleAxis (-90, new Vector3 (1, 0, 0)));
						    enemyHighlight.SendMessage("Create", next);
						    _highlights.Add (enemyHighlight);
							_cameFrom.Add (next);
						}
						continue;
					} else {

						//if (next.Expansion > _maxExpansion) {
						if (current.Expansion >= _maxExpansion) {
							continue;
						} else {


							if (!_cameFrom.Contains (next))
								next.Expansion = current.Expansion + 1;
						}

						if (!_cameFrom.Contains (next) && current.Expansion < _maxExpansion) {
							_frontier.Enqueue (next);
							_cameFrom.Add (next);


							if (next.Expansion <= _maxExpansion && next.HasBuilding == false) {
								//Add the walkable tile indication here
								GameObject walkHighlight = (GameObject)Instantiate (_walkHighlightPrefab, next.Position, Quaternion.AngleAxis (-90, new Vector3 (1, 0, 0)));
							    walkHighlight.SendMessage("Create", next);

							    _highlights.Add (walkHighlight);
							}
						}

					}
					//Debug.Log (next.Index);
				}
			}
		}
		yield return null;
	}

	public void ClearHighlights () {
		foreach (var highlight in _highlights) {
			Destroy (highlight);
		}
		_highlights = new List <GameObject> ();
	}
}
