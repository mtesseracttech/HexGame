using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KeyBindings : MonoBehaviour {

	private GameObject currentKey;

	[SerializeField]
	Text forward, backward, left, right, rotateLeft, rotateRight, inv1, inv2, inv3, inv4, inv5, inv6;

	KeyCode forwardKey, backwardKey, leftKey, rightKey, rotateLeftKey, rotateRightKey, inv1Key, inv2Key, inv3Key, inv4Key, inv5Key, inv6Key;

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
		inv1Key 		= (KeyCode)PlayerPrefs.GetInt ("inv1Key");
		inv2Key 		= (KeyCode)PlayerPrefs.GetInt ("inv2Key");
		inv3Key 		= (KeyCode)PlayerPrefs.GetInt ("inv3Key");
		inv4Key 		= (KeyCode)PlayerPrefs.GetInt ("inv4Key");
		inv5Key 		= (KeyCode)PlayerPrefs.GetInt ("inv5Key");
		inv6Key 		= (KeyCode)PlayerPrefs.GetInt ("inv6Key");
	}

	void Start () {
		GControlls.SetInitialKeyCodes ();

//		forward.text 		= forwardKey.ToString ();
//		backward.text 		= backwardKey.ToString ();
//		left.text 			= leftKey.ToString ();
//		right.text 			= rightKey.ToString ();
		rotateLeft.text 	= rotateLeftKey.ToString ();
		rotateRight.text 	= rotateRightKey.ToString ();
		inv1.text 			= inv1Key.ToString ();
		inv2.text 			= inv2Key.ToString ();
		inv3.text 			= inv3Key.ToString ();
		inv4.text 			= inv4Key.ToString ();
		inv5.text 			= inv5Key.ToString ();
		inv6.text 			= inv6Key.ToString ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Player Prefs: " + (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey") + " " + (KeyCode)PlayerPrefs.GetInt ("rotateRightKey") + " " +
				(KeyCode)PlayerPrefs.GetInt ("inv1Key") + " " + (KeyCode)PlayerPrefs.GetInt ("inv2Key") + " " + (KeyCode)PlayerPrefs.GetInt ("inv3Key") + " " +
				(KeyCode)PlayerPrefs.GetInt ("inv4Key") + " " +(KeyCode)PlayerPrefs.GetInt ("inv5Key") + " " + (KeyCode)PlayerPrefs.GetInt ("inv6Key")
			);

			Debug.Log (
				"Actual called keys: " + 
				" : " + GControlls.GetKey (Key.ROTATELEFT) +
				" " + GControlls.GetKey (Key.ROTATERIGHT) +
				" " + GControlls.GetKey (Key.INV1) +
				" " + GControlls.GetKey (Key.INV2) +
				" " + GControlls.GetKey (Key.INV3) +
				" " + GControlls.GetKey (Key.INV4) +
				" " + GControlls.GetKey (Key.INV5) +
				" " + GControlls.GetKey (Key.INV6)
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
					case "Inv 1":
						GControlls.SetKey (Key.INV1, e.keyCode);
						break;
					case "Inv 2":
						GControlls.SetKey (Key.INV2, e.keyCode);
						break;
					case "Inv 3":
						GControlls.SetKey (Key.INV3, e.keyCode);
						break;
					case "Inv 4":
						GControlls.SetKey (Key.INV4, e.keyCode);
						break;
					case "Inv 5":
						GControlls.SetKey (Key.INV5, e.keyCode);
						break;
					case "Inv 6":
						GControlls.SetKey (Key.INV6, e.keyCode);
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
//		KeyCode _forward = (KeyCode)PlayerPrefs.GetInt ("forwardKey");
//		forward.text = _forward.ToString ();
//		KeyCode _backward = (KeyCode)PlayerPrefs.GetInt ("backwardKey");
//		backward.text = _backward.ToString ();
//		KeyCode _left = (KeyCode)PlayerPrefs.GetInt ("leftKey");
//		left.text = _left.ToString ();
//		KeyCode _right = (KeyCode)PlayerPrefs.GetInt ("rightKey");
//		right.text = _right.ToString ();
		KeyCode _rotateLeft = (KeyCode)PlayerPrefs.GetInt ("rotateLeftKey");
		rotateLeft.text = _rotateLeft.ToString ();
		KeyCode _rotateRight = (KeyCode)PlayerPrefs.GetInt ("rotateRightKey");
		rotateRight.text = _rotateRight.ToString ();
		KeyCode _inv1 = (KeyCode)PlayerPrefs.GetInt ("inv1Key");
		inv1.text = _inv1.ToString ();
		KeyCode _inv2 = (KeyCode)PlayerPrefs.GetInt ("inv2Key");
		inv2.text = _inv2.ToString ();
		KeyCode _inv3 = (KeyCode)PlayerPrefs.GetInt ("inv3Key");
		inv3.text = _inv3.ToString ();
		KeyCode _inv4 = (KeyCode)PlayerPrefs.GetInt ("inv4Key");
		inv4.text = _inv4.ToString ();
		KeyCode _inv5 = (KeyCode)PlayerPrefs.GetInt ("inv5Key");
		inv5.text = _inv5.ToString ();
		KeyCode _inv6 = (KeyCode)PlayerPrefs.GetInt ("inv6Key");
		inv6.text = _inv6.ToString ();
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

		PlayerPrefs.SetInt ("inv1Key", (int)KeyCode.Alpha1);
		inv1Key = (KeyCode)PlayerPrefs.GetInt ("inv1Key");

		PlayerPrefs.SetInt ("inv2Key", (int)KeyCode.Alpha2);
		inv2Key = (KeyCode)PlayerPrefs.GetInt ("inv2Key");

		PlayerPrefs.SetInt ("inv3Key", (int)KeyCode.Alpha3);
		inv3Key = (KeyCode)PlayerPrefs.GetInt ("inv3Key");

		PlayerPrefs.SetInt ("inv4Key", (int)KeyCode.Alpha4);
		inv4Key = (KeyCode)PlayerPrefs.GetInt ("inv4Key");

		PlayerPrefs.SetInt ("inv5Key", (int)KeyCode.Alpha5);
		inv5Key = (KeyCode)PlayerPrefs.GetInt ("inv5Key");

		PlayerPrefs.SetInt ("inv6Key", (int)KeyCode.Alpha6);
		inv6Key = (KeyCode)PlayerPrefs.GetInt ("inv6Key");

		RefreshText ();
		GControlls.SetInitialKeyCodes ();
	}
}