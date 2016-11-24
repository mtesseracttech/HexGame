using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Key { FORWARD, BACKWARD, LEFT, RIGHT, ROTATELEFT, ROTATERIGHT, INV1, INV2, INV3, INV4, INV5, INV6 };

public static class GControlls {

	static private Dictionary <string, KeyCode> keys = new Dictionary<string, KeyCode> ();

	static KeyCode forward, backward, left, right, rotateLeft, rotateRight, inv1, inv2, inv3, inv4, inv5, inv6;

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
		case Key.INV1:
			inv1 = code;
			SetPlayerPref ("inv1Key", code);
			break;
		case Key.INV2:
			inv2 = code;
			SetPlayerPref ("inv2Key", code);
			break;
		case Key.INV3:
			inv3 = code;
			SetPlayerPref ("inv3Key", code);
			break;
		case Key.INV4:
			inv4 = code;
			SetPlayerPref ("inv4Key", code);
			break;
		case Key.INV5:
			inv5 = code;
			SetPlayerPref ("inv5Key", code);
			break;
		case Key.INV6:
			inv6 = code;
			SetPlayerPref ("inv6Key", code);
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
		case Key.INV1:
			return inv1;
		case Key.INV2:
			return inv2;
		case Key.INV3:
			return inv3;
		case Key.INV4:
			return inv4;
		case Key.INV5:
			return inv5;
		case Key.INV6:
			return inv6;
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
		    keyCode != (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey") && keyCode != (KeyCode)PlayerPrefs.GetInt ("rotateRightKey") &&
			keyCode != (KeyCode)PlayerPrefs.GetInt ("inv1Key") && keyCode != (KeyCode)PlayerPrefs.GetInt ("inv2Key") &&
			keyCode != (KeyCode)PlayerPrefs.GetInt ("inv3Key") && keyCode != (KeyCode)PlayerPrefs.GetInt ("inv4tKey") &&
			keyCode != (KeyCode)PlayerPrefs.GetInt ("inv5Key") && keyCode != (KeyCode)PlayerPrefs.GetInt ("inv6Key")
		) {
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
		inv1 			= (KeyCode)PlayerPrefs.GetInt ("inv1Key");
		inv2 			= (KeyCode)PlayerPrefs.GetInt ("inv2Key");
		inv3 			= (KeyCode)PlayerPrefs.GetInt ("inv3Key");
		inv4 			= (KeyCode)PlayerPrefs.GetInt ("inv4Key");
		inv5 			= (KeyCode)PlayerPrefs.GetInt ("inv5Key");
		inv6 			= (KeyCode)PlayerPrefs.GetInt ("inv6Key");
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
		case "Inv 1":
			NullKey (setKey);
			SetKey (Key.INV1, setKey);
			break;
		case "Inv 2":
			NullKey (setKey);
			SetKey (Key.INV2, setKey);
			break;
		case "Inv 3":
			NullKey (setKey);
			SetKey (Key.INV3, setKey);
			break;
		case "Inv 4":
			NullKey (setKey);
			SetKey (Key.INV4, setKey);
			break;
		case "Inv 5":
			NullKey (setKey);
			SetKey (Key.INV5, setKey);
			break;
		case "Inv 6":
			NullKey (setKey);
			SetKey (Key.INV6, setKey);
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
		if (inv1 == setKey) {
			SetKey (Key.INV1, KeyCode.None);
		}
		if (inv2 == setKey) {
			SetKey (Key.INV2, KeyCode.None);
		}
		if (inv3 == setKey) {
			SetKey (Key.INV3, KeyCode.None);
		}
		if (inv4 == setKey) {
			SetKey (Key.INV4, KeyCode.None);
		}
		if (inv5 == setKey) {
			SetKey (Key.INV5, KeyCode.None);
		}
		if (inv6 == setKey) {
			SetKey (Key.INV6, KeyCode.None);
		}
	}
}
