using UnityEngine;

public class HexMapCamera : MonoBehaviour {

	[SerializeField]
	HexGrid _grid;
	[SerializeField]
	float _stickMinZoom, _stickMaxZoom;
	[SerializeField]
	float _swivelMinZoom, _swivelMaxZoom;
	[SerializeField]
	float _moveSpeedMinZoom, _moveSpeedMaxZoom;
	[SerializeField]
	float _rotationSpeed;

	Transform _swivel, _stick;
	float _zoom = 1f;
	float _rotationAngle;

	void Awake () {
		_swivel = transform.GetChild(0);
		_stick = _swivel.GetChild(0);
	}

	void Update () {
		float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
		if (zoomDelta != 0f) {
			AdjustZoom(zoomDelta);
		}

		float rotationDelta = Input.GetAxis("Rotation");
		if (rotationDelta != 0f) {
			AdjustRotation(rotationDelta);
		}


		float xDelta = Input.GetAxis("Horizontal");
		float zDelta = Input.GetAxis("Vertical");
		if (xDelta != 0f || zDelta != 0f) {
			AdjustPosition(xDelta, zDelta);
		}
	}

	void AdjustZoom (float delta) {
		_zoom = Mathf.Clamp01 (_zoom + delta);

		float distance = Mathf.Lerp(_stickMinZoom, _stickMaxZoom, _zoom);
		_stick.localPosition = new Vector3(0f, 0f, distance);

		float angle = Mathf.Lerp(_swivelMinZoom, _swivelMaxZoom, _zoom);
		_swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
	}

	void AdjustPosition (float xDelta, float zDelta) {
		Vector3 direction = transform.localRotation * new Vector3 (xDelta, 0f, zDelta).normalized;
		float damping = Mathf.Max (Mathf.Abs (xDelta), Mathf.Abs (zDelta));
		float distance = Mathf.Lerp(_moveSpeedMinZoom, _moveSpeedMaxZoom, _zoom) * damping * Time.deltaTime;

		Vector3 position = transform.localPosition;
		position += direction * distance;
		transform.localPosition = ClampPosition (position);
	}

	Vector3 ClampPosition (Vector3 position) {
		float xMax = (_grid.chunkCountX * HexMetrics.chunkSizeX - 0.5f) * (2f * HexMetrics.innerRadius);
		position.x = Mathf.Clamp(position.x, 0f, xMax);

		float zMax = (_grid.chunkCountZ * HexMetrics.chunkSizeZ - 1) * (1.5f * HexMetrics.outerRadius);
		position.z = Mathf.Clamp(position.z, 0f, zMax);

		return position;
	}

	void AdjustRotation (float delta) {
		_rotationAngle += delta * _rotationSpeed * Time.deltaTime;
		transform.localRotation = Quaternion.Euler(0f, _rotationAngle, 0f);
	}
}
