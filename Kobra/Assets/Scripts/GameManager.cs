using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* GameManager: 
 * Calls the functions that are necessary for the game to be played.
 */

public class GameManager : MonoBehaviour {

	public SnakeHead player;
	public Mouse mouse;

	private void Start() {
        player = FindObjectOfType<SnakeHead>();
        mouse = FindObjectOfType<Mouse> ();

		InvokeRepeating ("TimerInvoke", 0, 0.1f);
	}

	private void FixedUpdate() {
		if (Input.anyKey) {
			player.ChangeDir ();
            player.Shoot();
		}
	}

	private void TimerInvoke() {
		player.Move ();

	}

}
