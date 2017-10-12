using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Snake player;
	public Mouse mouse;
	public GameObject snakePrefab;
    public GameObject bulletPrefab;

    public Snake tail;
    public ObjectPool pool;

    private Vector3 nextPos;
	//public int direction;
    private enum Direction {
        Up,
        Down,
        Left, 
        Right,
    }

    Direction direction;

	private int currentLength;
	public int maxLength;

	void Start() {
        player = FindObjectOfType<Snake>();
        mouse = FindObjectOfType<Mouse> ();
		pool = GetComponent<ObjectPool> ();

		maxLength = 4;
		currentLength = 1;

		InvokeRepeating ("TimerInvoke", 0, 0.1f);
	}

	void Update() {
		if (Input.anyKey) {
			ChangeDir ();
            Shoot();
		}

		if (player.transform.position == mouse.transform.position) {
			EventManager.StartEvent ();
		}
	}

	void TimerInvoke() {
		Move ();

		if (currentLength >= maxLength) {
			TailFunction ();
		} else {
			currentLength++;
		}
	}
    
	public void Move() {
		GameObject temp;
		nextPos = player.transform.position;

        switch (direction) {
            case Direction.Up:
			//move up
			nextPos = new Vector3 (nextPos.x, nextPos.y, nextPos.z + 1);
			break;

		case Direction.Down:
			//move down
			nextPos = new Vector3 (nextPos.x, nextPos.y, nextPos.z - 1);
			break;

		case Direction.Left:
			//move left
			nextPos = new Vector3 (nextPos.x - 1, nextPos.y, nextPos.z);
			break;

		case Direction.Right:
			//move right
			nextPos = new Vector3 (nextPos.x + 1, nextPos.y, nextPos.z);
			break;
		}
        //Move the head to the newly instantiated snake part
        temp = pool.GetObject(nextPos);
		player.SetNext (temp.GetComponent<Snake>());
		player = temp.GetComponent<Snake> ();

		return;
	}

	void ChangeDir() {
		if (Input.GetKey (KeyCode.W) && direction != Direction.Down) {
			direction = Direction.Up;
		} else if (Input.GetKey (KeyCode.S) && direction != Direction.Up) {
			direction = Direction.Down;
		} else if (Input.GetKey (KeyCode.A) && direction != Direction.Right) {
			direction = Direction.Left;
		} else if (Input.GetKey (KeyCode.D) && direction != Direction.Left) {
			direction = Direction.Right;
		}
	}

	void TailFunction() {
		//makes sure the tail is always the last gameobject
		Snake tempSnake = tail;
		tail = tail.GetNext ();
		tempSnake.RemoveTail ();
	}
    
	void OnEnable() {
		EventManager.DeathEvent += AddBody;
	}

	void OnDisable() {
		EventManager.DeathEvent -= AddBody;
	} 

	public void AddBody() {
		maxLength++;
	}

    public void Shoot() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Shoot!");
            Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);
        }
    }
}
