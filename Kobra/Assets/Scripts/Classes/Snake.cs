using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Snake: 
 * Keeps track of the direction, position and lenght of the snake.
 * Tells the bullets when to spawn.
 */

public class Snake : MonoBehaviour, IDamagable {

    public GameObject bulletPrefab;
    public Bullet bullet;
    private ObjectPool pool;
    private Vector3 spawnPos;

    private int currentLength;
    public int maxLength;

    public GameObject snakeBody;
    
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


        maxLength = 4;
        currentLength = 1;
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet" || col.gameObject.tag == "Mouse") {
            Die();
        }
    }

	//the snake dies when the head hits its body, a bullet hits it, or when it hits the mouse
    //Dying redirects to the gameover screen
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
        bullet.ChangeDir(direction);

        //Move the head to the newly instantiated snake part
        temp = pool.GetObject(nextPos);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        if (currentLength >= maxLength) {
            TailFunction();
        }
        else {
            head.currentLength++;
        }

        return;
    }

    //Set the enum to a new direction
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
    
    public void Shoot() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //The bullet kills the player, therefore there needs to be enough space between it and the player when it spawns
            switch (direction) {
                case Direction.Up:
                    //move up
                    spawnPos = new Vector3(nextPos.x, nextPos.y, nextPos.z + 2);
                    break;

                case Direction.Down:
                    //move down
                    spawnPos = new Vector3(nextPos.x, nextPos.y, nextPos.z - 2);
                    break;

                case Direction.Left:
                    //move left
                    spawnPos = new Vector3(nextPos.x - 2, nextPos.y, nextPos.z);
                    break;

                case Direction.Right:
                    //move right
                    spawnPos = new Vector3(nextPos.x + 2, nextPos.y, nextPos.z);
                    break;
            }

            Instantiate(bulletPrefab, spawnPos, transform.rotation);
        }
    }
    
    public void TailFunction() {
        //makes sure the tail is always the last gameobject
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }

    void OnEnable() {
        EventManager.DeathEvent += AddBody;
    }

    void OnDisable() {
        EventManager.DeathEvent -= AddBody;
    }

    public void AddBody() {
        head.maxLength++;
    }
}
