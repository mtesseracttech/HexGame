using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Rendering
{
    public class NodeHighlighter : MonoBehaviour
    {

        [SerializeField]
        private int                       _maxExpansion;
        [SerializeField]
        private GameObject                _walkHighlightPrefab;
        [SerializeField]
        private GameObject                _attackHighlightPrefab;
        [SerializeField]
        private GameObject                _terrainHighlightPrefab;

        private Queue <HexNode>           _frontier;
        private HashSet <HexNode>         _cameFrom;
        private List <GameObject>         _highlights               = new List<GameObject> ();

        public IEnumerator Search (HexNode root)
        {
            ClearHighlights ();
            root.Expansion = 0;

            if (root.HasBuilding) {}
            else
            {
                _frontier = new Queue<HexNode> ();
                _cameFrom = new HashSet<HexNode> ();

                _frontier.Enqueue (root);
                _cameFrom.Add (root);

                while (_frontier.Count > 0)
                {
                    var current = _frontier.Dequeue ();

                    foreach (var next in current.Neighbors)
                    {
                        if (_cameFrom.Contains (next) || next.HasBuilding || next.HasEnemy)
                        {
                            if (next.HasBuilding && !_cameFrom.Contains (next) && current.Expansion < _maxExpansion) {
                                GameObject terrainHighlight = Instantiate (_terrainHighlightPrefab, next.Position, Quaternion.AngleAxis (-90, new Vector3 (1, 0, 0)));
                                terrainHighlight.SendMessage("Create", next);
                                _highlights.Add (terrainHighlight);
                                _cameFrom.Add   (next);
                            }

                            if (next.HasEnemy && !_cameFrom.Contains (next) && current.Expansion < _maxExpansion) {
                                GameObject enemyHighlight = Instantiate (_attackHighlightPrefab, next.Position, Quaternion.AngleAxis (-90, new Vector3 (1, 0, 0)));
                                enemyHighlight.SendMessage("Create", next);
                                _highlights.Add (enemyHighlight);
                                _cameFrom.Add   (next);
                            }
                        }
                        else
                        {

                            //if (next.Expansion > _maxExpansion) {
                            if (current.Expansion >= _maxExpansion)
                            {
                                continue;
                            }
                            if (!_cameFrom.Contains (next))
                                next.Expansion = current.Expansion + 1;

                            if (!_cameFrom.Contains (next) && current.Expansion < _maxExpansion)
                            {
                                _frontier.Enqueue (next);
                                _cameFrom.Add (next);


                                if (next.Expansion <= _maxExpansion && next.HasBuilding == false)
                                {
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

        public void ClearHighlights ()
        {
            foreach (var highlight in _highlights) Destroy(highlight);
            _highlights = new List <GameObject> ();
        }
    }
}
