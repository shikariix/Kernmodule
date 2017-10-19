using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Snake player;
	public Mouse mouse;

	void Start() {
		player = FindObjectOfType<Snake> ();
		mouse = FindObjectOfType<Mouse> ();
	}

	void Update() {
		player.Move ();
		if (player.transform.position == mouse.transform.position) {
			EventManager.StartEvent ();
		}
	}

}
