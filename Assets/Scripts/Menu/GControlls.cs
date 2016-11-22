using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Key { FORWARD, BACKWARD, LEFT, RIGHT, ROTATELEFT, ROTATERIGHT };

public static class GControlls {

	static private Dictionary <string, KeyCode> keys = new Dictionary<string, KeyCode> ();

	static KeyCode forward, backward, left, right, rotateLeft, rotateRight;

	public static void SetKey (Key key, KeyCode code) {
		switch (key) {
		case Key.FORWARD:
			forward = code;
			SetPlayerPref ("forwardKey", code);
			break;
		case Key.BACKWARD:
			backward = code;
			SetPlayerPref ("backwardKey", code);
			break;
		case Key.LEFT:
			left = code;
			SetPlayerPref ("leftKey", code);
			break;
		case Key.RIGHT:
			right = code;
			SetPlayerPref ("rightKey", code);
			break;
		case Key.ROTATELEFT:
			rotateLeft = code;
			SetPlayerPref ("rotateLeftKey", code);
			break;
		case Key.ROTATERIGHT:
			rotateRight = code;
			SetPlayerPref ("rotateRightKey", code);
			break;
		default:
			break;
		}
	}

	public static KeyCode GetKey (Key key) {
		switch (key) {
		case Key.FORWARD:
			return forward;
		case Key.BACKWARD:
			return backward;
		case Key.LEFT:
			return left;
		case Key.RIGHT:
			return right;
		case Key.ROTATELEFT:
			return rotateLeft;
		case Key.ROTATERIGHT:
			return rotateRight;
		default:
			break;
		}

		return KeyCode.None;
	}

	static void SetPlayerPref (string keyName, KeyCode keyCode) {
		PlayerPrefs.SetInt (keyName, (int)keyCode);
	}

	public static bool IsKeyAlreadySet (KeyCode keyCode) {
		if (keyCode != (KeyCode)PlayerPrefs.GetInt ("forwardKey") && keyCode != (KeyCode)PlayerPrefs.GetInt ("backwardKey") &&
		    keyCode != (KeyCode)PlayerPrefs.GetInt ("leftKey") && keyCode != (KeyCode)PlayerPrefs.GetInt ("rightKey") &&
		    keyCode != (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey") && keyCode != (KeyCode)PlayerPrefs.GetInt ("rotateRightKey")) {
			return false;
		} else {
			return true;
		}
	}

	public static void SetInitialKeyCodes () {
		forward 		= (KeyCode)PlayerPrefs.GetInt ("forwardKey");
		backward 		= (KeyCode)PlayerPrefs.GetInt ("backwardKey");
		left 			= (KeyCode)PlayerPrefs.GetInt ("leftKey");
		right 			= (KeyCode)PlayerPrefs.GetInt ("rightKey");
		rotateLeft 		= (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey");
		rotateRight 	= (KeyCode)PlayerPrefs.GetInt ("rotateRightKey");
	}

	public static void SetKeyNullOther (string keyCurrentlySet, KeyCode setKey) {
		switch (keyCurrentlySet) {
		case "Forward":
			NullKey (setKey);
			SetKey (Key.FORWARD, setKey);
			break;
		case "Backward":
			NullKey (setKey);
			SetKey (Key.BACKWARD, setKey);
			break;
		case "Left":
			NullKey (setKey);
			SetKey (Key.LEFT, setKey);
			break;
		case "Right":
			NullKey (setKey);
			SetKey (Key.RIGHT, setKey);
			break;
		case "Rotate Left":
			NullKey (setKey);
			SetKey (Key.ROTATELEFT, setKey);
			break;
		case "Rotate Right":
			NullKey (setKey);
			SetKey (Key.ROTATERIGHT, setKey);
			break;
		default:
			break;
		}
	}

	static void NullKey (KeyCode setKey) {
		if (forward == setKey) {
			SetKey (Key.FORWARD, KeyCode.None);
		}
		if (backward == setKey) {
			SetKey (Key.BACKWARD, KeyCode.None);
		}
		if (left == setKey) {
			SetKey (Key.LEFT, KeyCode.None);
		}
		if (right == setKey) {
			SetKey (Key.RIGHT, KeyCode.None);
		}
		if (rotateLeft == setKey) {
			SetKey (Key.ROTATELEFT, KeyCode.None);
		}
		if (rotateRight == setKey) {
			SetKey (Key.ROTATERIGHT, KeyCode.None);
		}
	}
}
