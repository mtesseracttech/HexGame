using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	//Button panels
	[SerializeField]
	GameObject menuButtonPanel;
	[SerializeField]
	GameObject optionsButtonPanel;

	//CREDITS Panel
	[SerializeField]
	GameObject creditsPanel;

	//OPTIONS Keybind
	[SerializeField]
	GameObject keybindPanel;

	void Awake () {
		creditsPanel.SetActive (false);
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
		menuButtonPanel.SetActive (true);
	}

	void Update () {
		
	}

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

	//Main panel
	public void OptionsMenu () {
		creditsPanel.SetActive (false);
		menuButtonPanel.SetActive (false);
		optionsButtonPanel.SetActive (true);
	}

	//Credits panel
	public void CreditsPanel () {
		creditsPanel.SetActive (true);
	}


	//Options Menu Panel
	public void KeybindPanel () {
		keybindPanel.SetActive (true);
	}

	public void BackToMainMenu () {
		creditsPanel.SetActive (false);
		keybindPanel.SetActive (false);
		optionsButtonPanel.SetActive (false);
		menuButtonPanel.SetActive (true);
	}

	public void ExitGame () {
		Application.Quit ();
	}
}
