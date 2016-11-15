using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType
{

    public string Name;
    public GameObject TileVisualPrefab;

    public bool IsWalkable = true;
    public float MovementCost = 1;

}
