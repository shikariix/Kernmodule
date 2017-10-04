using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public static SceneChanger Instance { get; private set; }

	//makes sure there is always one (1) SceneChanger in the scene
	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
	}


	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameMenu ();
		}
	}


	public void PlayGame() {
		SceneManager.LoadScene ("main");
	}

	public void GameMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void ExitGame() {
		Application.Quit ();
		Debug.Log ("You cannot quit in the editor.");
	}
}
