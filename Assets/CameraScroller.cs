using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{

    public GameObject  Camera;
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public float duration = 1.0f;
    private Vector3[]  _positions;
    private int        _currentPosIndex;
    private Vector3    _target;
    private Vector3    _current;

    float _timeSinceStart;

    // Use this for initialization
    private void Start ()
	{
        List<Vector3> children = new List<Vector3>();
	    foreach (Transform child in transform)
	    {
	        children.Add(child.position);
	    }
	    _positions = children.ToArray();

        _currentPosIndex = 0;
	    _current = _positions[_currentPosIndex];
	    _target = _positions[_currentPosIndex + 1];
        Camera.transform.position = _current;
        _timeSinceStart = 0.0f;

    }
	
	// Update is called once per frame
	private void Update ()
    {
        _timeSinceStart += Time.deltaTime;
        float s = _timeSinceStart / duration;

        if (Vector3.Distance(Camera.transform.position, _target) > 0.01f)
        {
            Camera.transform.position = Vector3.Lerp(_current, _target, curve.Evaluate(s));
        }
        else
        {
            if (_currentPosIndex + 1 < _positions.Length - 1)
            {
                _currentPosIndex++;
                _current = _positions[_currentPosIndex];
                _target = _positions[_currentPosIndex + 1];
                _timeSinceStart = 0.0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentPosIndex + 1 < _positions.Length - 1)
            {
                _currentPosIndex++;
                _current = _positions[_currentPosIndex];
                _target = _positions[_currentPosIndex + 1];
                _timeSinceStart = 0.0f;
            }
        }
	}

    public static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }
}
