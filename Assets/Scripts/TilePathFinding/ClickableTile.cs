using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
