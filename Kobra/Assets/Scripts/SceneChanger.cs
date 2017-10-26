using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Scene Changer:
 * Public functions for buttons to use to change to another scene.
 * Also used when the player dies to change the scene to the GameOver scene.
 */

public class SceneChanger : MonoBehaviour {
    
	public void PlayGame() {
		SceneManager.LoadScene ("main");
	}

	public void GameMenu() {
		SceneManager.LoadScene ("Menu");
	}

    public static void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

	public void ExitGame() {
		Application.Quit ();
		Debug.Log ("You cannot quit in the editor.");
	}
}
