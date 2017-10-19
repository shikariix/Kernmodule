using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* SnakeHead:
 * The brain of the snake. Manages direction, positioning, and length. Basically everything useful.
 */

public class SnakeHead : MonoBehaviour {

    public GameObject bulletPrefab;
    public Bullet bullet;
    private ObjectPool pool;
    private Vector3 spawnPos;

    private int currentLength = 1;
    public int maxLength = 4;

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

    private Vector3 goUp = new Vector3(0, 0, 1);
    private Vector3 goDown = new Vector3(0, 0, -1);
    private Vector3 goLeft = new Vector3(-1, 0, 0);
    private Vector3 goRight = new Vector3(1, 0, 0);

    void Start() {
        head = FindObjectOfType<Snake>();
        pool = FindObjectOfType<ObjectPool>();

        EventManager.DeathEvent += AddBody;

}

    public void Move() {
        GameObject temp;
        nextPos = head.transform.position;

        switch (direction) {
            case Direction.Up:
                nextPos += goUp;
                break;

            case Direction.Down:
                nextPos += goDown;
                break;

            case Direction.Left:
                nextPos += goLeft;
                break;

            case Direction.Right:
                nextPos += goRight;
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
            currentLength++;
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
                    spawnPos = nextPos + (goUp * 2);
                    break;

                case Direction.Down:
                    //move down
                    spawnPos = nextPos + (goDown * 2);
                    break;

                case Direction.Left:
                    //move left
                    spawnPos = nextPos + (goLeft * 2);
                    break;

                case Direction.Right:
                    //move right
                    spawnPos = nextPos + (goRight * 2);
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

    public void AddBody() {
        maxLength++;
    }
}
