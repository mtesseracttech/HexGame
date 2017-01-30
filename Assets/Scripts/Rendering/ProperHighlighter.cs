using System.Collections.Generic;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Rendering
{
    public class ProperHighlighter : MonoBehaviour
    {
        public GameObject PlayerHighlight;
        public GameObject EnemyHighlight;
        public GameObject NpcHighlight;
        public GameObject PropHighlight;
        public GameObject BuildingHighlight;
        public GameObject WalkHighlight;
        public GameObject TerrainHighlight;

        private BreadthFirst  _breadthFirst;

        private List<GameObject> _highlights;

        private readonly Quaternion _hlrot = Quaternion.AngleAxis(-90, Vector3.right);

        private void Start()
        {
            _breadthFirst = new BreadthFirst();
            _highlights   = new List<GameObject>();
        }

        public void ShowHighlight(HexNode currentNode, int maxExpansion)
        {
            maxExpansion += 1;
            _breadthFirst.Search(currentNode, maxExpansion);
            if (_breadthFirst.Done)
            {
                foreach (var node in _breadthFirst.Nodes)
                {
                    GameObject nodeHighlight = null;
                    if (node.HasOccupant)
                    {
                        if      (node.HasPlayer  ) nodeHighlight = PlayerHighlight;
                        else if (node.HasEnemy   ) nodeHighlight = EnemyHighlight;
                        else if (node.HasNPC     ) nodeHighlight = NpcHighlight;
                        else if (node.HasBuilding) nodeHighlight = BuildingHighlight;
                        else if (node.HasProp    ) nodeHighlight = PropHighlight;
                    }
                    else
                    {
                        if   (node.Expansion == 1) nodeHighlight = WalkHighlight;
                        else                       nodeHighlight = TerrainHighlight;
                    }
                    if (nodeHighlight != null)
                    {
                        GameObject highlight = Instantiate(nodeHighlight, node.Position, _hlrot);
                        highlight.SendMessage("Create", node);
                        _highlights.Add(highlight);
                    }
                }
            }
        }

        public void DestroyHighlights()
        {
            foreach (var highlight in _highlights) Destroy(highlight);
            _highlights = new List<GameObject>();
        }
    }
}