using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KeyBindings : MonoBehaviour {

	private GameObject currentKey;

	[SerializeField]
	Text forward, backward, left, right, rotateLeft, rotateRight;

	KeyCode forwardKey, backwardKey, leftKey, rightKey, rotateLeftKey, rotateRightKey;

	void Awake () {
		int hasPlayed = PlayerPrefs.GetInt ("Has Played");
		if (hasPlayed == 0) {
			SetDefaults ();
			PlayerPrefs.SetInt ("Has Played", 1);
		}

		forwardKey 		= (KeyCode)PlayerPrefs.GetInt ("forwardKey");
		backwardKey 	= (KeyCode)PlayerPrefs.GetInt ("backwardKey");
		leftKey 		= (KeyCode)PlayerPrefs.GetInt ("leftKey");
		rightKey 		= (KeyCode)PlayerPrefs.GetInt ("rightKey");
		rotateLeftKey 	= (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey");
		rotateRightKey 	= (KeyCode)PlayerPrefs.GetInt ("rotateRightKey");
	}

	void Start () {
		GControlls.SetInitialKeyCodes ();

		forward.text 		= forwardKey.ToString ();
		backward.text 		= backwardKey.ToString ();
		left.text 			= leftKey.ToString ();
		right.text 			= rightKey.ToString ();
		rotateLeft.text 	= rotateLeftKey.ToString ();
		rotateRight.text 	= rotateRightKey.ToString ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Player Prefs: " + (KeyCode)PlayerPrefs.GetInt ("forwardKey") + " " + (KeyCode)PlayerPrefs.GetInt ("backwardKey") + " " + 
				(KeyCode)PlayerPrefs.GetInt ("leftKey") + " " + (KeyCode)PlayerPrefs.GetInt ("rightKey") + " " + 
				(KeyCode)PlayerPrefs.GetInt ("rotateLeftKey") + " " + (KeyCode)PlayerPrefs.GetInt ("rotateRightKey"));

			Debug.Log (
				"Actual called keys: " + " : " + GControlls.GetKey (Key.FORWARD) +
				" " + GControlls.GetKey (Key.BACKWARD) +
				" " + GControlls.GetKey (Key.LEFT) +
				" " + GControlls.GetKey (Key.RIGHT) +
				" " + GControlls.GetKey (Key.ROTATELEFT) +
				" " + GControlls.GetKey (Key.ROTATERIGHT)
			);
		}
	}

	void OnGUI() {
		if (currentKey != null) {
			Event e = Event.current;
			if (e.isKey) {
				
				if (GControlls.IsKeyAlreadySet (e.keyCode)) {
					GControlls.SetKeyNullOther (currentKey.transform.name, e.keyCode);
				} else {					
					switch (currentKey.transform.name) {
					case "Forward":	
						GControlls.SetKey (Key.FORWARD, e.keyCode);
						break;
					case "Backward":
						GControlls.SetKey (Key.BACKWARD, e.keyCode);
						break;
					case "Left":
						GControlls.SetKey (Key.LEFT, e.keyCode);
						break;
					case "Right":
						GControlls.SetKey (Key.RIGHT, e.keyCode);
						break;
					case "Rotate Left":
						GControlls.SetKey (Key.ROTATELEFT, e.keyCode);
						break;
					case "Rotate Right":
						GControlls.SetKey (Key.ROTATERIGHT, e.keyCode);
						break;
					default:
						break;
					}
				}
				currentKey = null;
				RefreshText ();
			}
		}
	}

	void RefreshText () {
		KeyCode _forward = (KeyCode)PlayerPrefs.GetInt ("forwardKey");
		forward.text = _forward.ToString ();
		KeyCode _backward = (KeyCode)PlayerPrefs.GetInt ("backwardKey");
		backward.text = _backward.ToString ();
		KeyCode _left = (KeyCode)PlayerPrefs.GetInt ("leftKey");
		left.text = _left.ToString ();
		KeyCode _right = (KeyCode)PlayerPrefs.GetInt ("rightKey");
		right.text = _right.ToString ();
		KeyCode _rotateLeft = (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey");
		rotateLeft.text = _rotateLeft.ToString ();
		KeyCode _rotateRight = (KeyCode)PlayerPrefs.GetInt ("rotateRightKey");
		rotateRight.text = _rotateRight.ToString ();
	}

	public void ChangeKey (GameObject clicked) {
		currentKey = clicked;
	}

	public void SetDefaults () {
		PlayerPrefs.SetInt ("forwardKey", (int)KeyCode.W);
		forwardKey = (KeyCode)PlayerPrefs.GetInt ("forwardKey");

		PlayerPrefs.SetInt ("backwardKey", (int)KeyCode.S);
		backwardKey = (KeyCode)PlayerPrefs.GetInt ("backwardKey");

		PlayerPrefs.SetInt ("leftKey", (int)KeyCode.A);
		leftKey = (KeyCode)PlayerPrefs.GetInt ("leftKey");

		PlayerPrefs.SetInt ("rightKey", (int)KeyCode.D);
		rightKey = (KeyCode)PlayerPrefs.GetInt ("rightKey");

		PlayerPrefs.SetInt ("rotateLeftKey", (int)KeyCode.Q);
		rotateLeftKey = (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey");

		PlayerPrefs.SetInt ("rotateRightKey", (int)KeyCode.E);
		rotateRightKey = (KeyCode)PlayerPrefs.GetInt ("rotateRightKey");

		RefreshText ();
		GControlls.SetInitialKeyCodes ();
	}
}
