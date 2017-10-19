using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IDamagable {

	private int length;
	private float speed;
	private Direction currentDir;
	private Direction oldDirection;
	private Vector3 dir;

	private Vector3 bodyPos;

	public GameObject bodyTarget;
	public GameObject snakeBody;

	private enum Direction {
		Up,
		Right,
		Down,
		Left,
	}

	//I need to identify the gameobject that I want to use as body
	//The length decides how many bodyparts there should be
	//The length can be increased and decreased

	// Use this for initialization
	void Start () {
		//ChangeDir (Direction.Up);
		speed = 0.5f;
		length = 0;
		ChangeDir (Direction.Up);
		//InvokeRepeating ("TimerInvoke", 0, 0.05f);
	}

	void OnEnable() {
		EventManager.DeathEvent += AddBody;
	}

	void OnDisable() {
		EventManager.DeathEvent -= AddBody;
	}

	//the snake dies when the head hits its body, a bullet hits it, or when it hits the mouse
	public void Die() {
		
	}

	public void AddBody() {
		length++;
		GameObject bodyPart = Instantiate (snakeBody, bodyTarget.transform.position, transform.rotation);
		bodyPart.transform.parent = transform;
		bodyTarget.transform.position += new Vector3 (0, 0, -1);
	}

	//set direction based on key pressed. A player cannot make a 180 turn.
	public void Move() {
		transform.position += dir;
		if (Input.GetKeyDown (KeyCode.UpArrow) && currentDir != Direction.Down && currentDir != Direction.Up) {
			ChangeDir (Direction.Up);
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && currentDir != Direction.Left && currentDir != Direction.Right) {
			ChangeDir (Direction.Right);
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && currentDir != Direction.Up && currentDir != Direction.Down) {
			ChangeDir (Direction.Down);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && currentDir != Direction.Right && currentDir != Direction.Left) {
			ChangeDir (Direction.Left);
		}
	}

	void ChangeDir(Direction newDirection) {
		oldDirection = currentDir;
		currentDir = newDirection;
		StartCoroutine (newDirection.ToString());
	}

	//edit position based on chosen direction
	IEnumerator Up() {
		
		dir = new Vector3 (0, 0, speed);
		if (oldDirection == Direction.Right) {
			transform.Rotate (0, -90, 0);
		} else if (oldDirection == Direction.Left) {
			transform.Rotate (0, 90, 0);
		}
		while (currentDir == Direction.Up) {
			yield return null;
		}
	}

	IEnumerator Down() {
		dir = new Vector3 (0, 0, -speed);
		if (oldDirection == Direction.Right) {
			transform.Rotate (0, 90, 0);
		} else if (oldDirection == Direction.Left) {
			transform.Rotate (0, -90, 0);
		}

		while (currentDir == Direction.Down) {
			yield return null;
		}
	}

	IEnumerator Right() {
		dir = new Vector3 (speed, 0, 0);
		if (oldDirection == Direction.Up) {
			transform.Rotate (0, 90, 0);
		} else if (oldDirection == Direction.Down) {
			transform.Rotate (0, -90, 0);
		}

		while (currentDir == Direction.Right) {
			yield return null;
		}
	}

	IEnumerator Left() {
		dir = new Vector3 (-speed, 0, 0);
		if (oldDirection == Direction.Down) {
			transform.Rotate (0, 90, 0);
		} else if (oldDirection == Direction.Up) {
			transform.Rotate (0, -90, 0);
		}

		while (currentDir == Direction.Left) {
			yield return null;
		}
	}
}
