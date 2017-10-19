using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* GameManager: 
 * Calls the functions that are necessary for the game to be played.
 */

public class GameManager : MonoBehaviour {

	public Snake player;
	public Mouse mouse;

	void Start() {
        player = FindObjectOfType<Snake>();
        mouse = FindObjectOfType<Mouse> ();

		InvokeRepeating ("TimerInvoke", 0, 0.1f);
	}

	void FixedUpdate() {
		if (Input.anyKey) {
			player.ChangeDir ();
            player.Shoot();
		}
	}

	void TimerInvoke() {
		player.Move ();

	}

}
