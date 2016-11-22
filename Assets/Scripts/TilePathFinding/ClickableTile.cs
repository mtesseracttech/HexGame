using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.TilePathFinding
{
    public class ClickableTile : MonoBehaviour {

        public int TileX;
        public int TileY;
        public TileMap Map;

        void OnMouseUp()
        {
            Debug.Log("Click!");

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Map.GeneratePathTo(TileX, TileY);
        }
    }
}
