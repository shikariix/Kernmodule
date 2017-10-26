using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public const int MAX_LENGTH = 150;
	public GameObject snakePrefab;

	public Snake[] objects;
	public int currentSnake = 0;

	private void Start() {
		objects = new Snake[MAX_LENGTH];
		for (int i = 0; i < MAX_LENGTH; ++i) {
            //this creates the objects all on the same position
            //position should be changed on load
			objects [i] = Instantiate (snakePrefab).GetComponent<Snake> ();
			objects [i].gameObject.SetActive (false);
		}
	}

	//returns part of snake in the given position so it can be used
	public GameObject GetObject(Vector3 pos) {
		//activate current snakepart, point it in the right direction
		Snake snake = objects[currentSnake];
        snake.gameObject.transform.position = pos;
		snake.gameObject.SetActive(true);

		//cycle through our snake, make sure to loop around
		if (++currentSnake == MAX_LENGTH) {
			currentSnake = 0;
		}

		return snake.gameObject;
	}

}
