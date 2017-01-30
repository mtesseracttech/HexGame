using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	//Button panels
	[SerializeField]
	GameObject optionsButtonPanel;

	//OPTIONS Keybind
	[SerializeField]
	GameObject keybindPanel;

	private bool isGamePaused = false;

	void Awake () {
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && isGamePaused == false) {
			OpenPauseMenu ();
		}
	}

	//Options Menu Panel
	public void KeybindPanel () {
		keybindPanel.SetActive (true);
	}

	public void ResumeGame () {
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
		isGamePaused = false;
	}

	public void OpenPauseMenu () {
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (true);
		isGamePaused = true;
	}

	public void BackToMainMenu () {
		SceneManager.LoadScene (0); //Fix once the scenes are all in order
	}
}
