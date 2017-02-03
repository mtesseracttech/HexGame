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

	[SerializeField]
	GameObject backdrop;

	private bool isGamePaused = false;

	void Awake () {
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
		backdrop.SetActive (false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && isGamePaused == false) {
			OpenPauseMenu ();
		} else if (Input.GetKeyDown (KeyCode.Escape) && isGamePaused == true) {
			ClosePauseMenu ();
		}
	}

	//Options Menu Panel
	public void KeybindPanel () {
		keybindPanel.SetActive (true);
	}

	public void ResumeGame () {
		backdrop.SetActive (false);
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
		isGamePaused = false;
	}

	public void OpenPauseMenu () {
		backdrop.SetActive (true);
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (true);
		isGamePaused = true;
	}

	public void ClosePauseMenu () {
		backdrop.SetActive (false);
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
		isGamePaused = false;
	}

	public void BackToMainMenu () {
		SceneManager.LoadScene (0); //Fix once the scenes are all in order
	}
}
