﻿using UnityEngine;

public class HexCell : MonoBehaviour {

	[SerializeField]
	HexCell[] _neighbors;

	public HexCoordinates coordinates;

	public Color color;


	public HexCell GetNeighbor (HexDirection direction) {
		return _neighbors [(int)direction];
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		_neighbors [(int)direction] = cell;
		cell._neighbors [(int)direction.Opposite ()] = this;
	}
}
