using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IDamagable {

    public GameObject bulletPrefab;

    public GameObject snakeBody;
    ObjectPool pool;
    public Snake head;
    public Snake tail;
    private Snake next;

    private Vector3 nextPos;
    public enum Direction {
        Up,
        Down,
        Left,
        Right,
    }

    public Direction direction;


    void Start() {
        head = FindObjectOfType<Snake>();
        pool = FindObjectOfType<ObjectPool>();
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            Die();
        }
    }

	//the snake dies when the head hits its body, a bullet hits it, or when it hits the mouse
	public void Die() {
        SceneChanger.GameOver();
	}

	public void SetNext(Snake IN) {
		next = IN;
	}

	public Snake GetNext() {
		return next;
	}

	public void RemoveTail() {
		gameObject.SetActive (false);
	}

    public void Move() {
        GameObject temp;
        nextPos = head.transform.position;

        switch (direction) {
            case Direction.Up:
                //move up
                nextPos = new Vector3(nextPos.x, nextPos.y, nextPos.z + 1);
                break;

            case Direction.Down:
                //move down
                nextPos = new Vector3(nextPos.x, nextPos.y, nextPos.z - 1);
                break;

            case Direction.Left:
                //move left
                nextPos = new Vector3(nextPos.x - 1, nextPos.y, nextPos.z);
                break;

            case Direction.Right:
                //move right
                nextPos = new Vector3(nextPos.x + 1, nextPos.y, nextPos.z);
                break;
        }
        //Move the head to the newly instantiated snake part
        temp = pool.GetObject(nextPos);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        return;
    }

    public void ChangeDir() {
        if (Input.GetKey(KeyCode.W) && direction != Direction.Down) {
            direction = Direction.Up;
        }
        else if (Input.GetKey(KeyCode.S) && direction != Direction.Up) {
            direction = Direction.Down;
        }
        else if (Input.GetKey(KeyCode.A) && direction != Direction.Right) {
            direction = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.D) && direction != Direction.Left) {
            direction = Direction.Right;
        }
    }

    public Direction GetDir() {
        return direction;
    }

    public void TailFunction() {
        //makes sure the tail is always the last gameobject
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }
    
    public void Shoot() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Shoot!");
            Instantiate(bulletPrefab, nextPos, transform.rotation);
        }
    }
}
