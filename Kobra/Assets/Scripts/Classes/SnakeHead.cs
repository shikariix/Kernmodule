using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* SnakeHead:
 * The brain of the snake. Manages direction, positioning, length, death, and also where the bullet spawns when it shoots.
 */

public class SnakeHead : MonoBehaviour {
    
    private ObjectPool pool;
    private BulletObjectPool bulletPool;
    private Vector3 spawnPos;

    public int currentLength = 1;
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

    //these vectors are used to move in the 4 different directions
    private Vector3 goUp = new Vector3(0, 0, 1);
    private Vector3 goDown = new Vector3(0, 0, -1);
    private Vector3 goLeft = new Vector3(-1, 0, 0);
    private Vector3 goRight = new Vector3(1, 0, 0);


    void Start() {
        head = FindObjectOfType<Snake>();
        pool = FindObjectOfType<ObjectPool>();
        bulletPool = FindObjectOfType<BulletObjectPool>();
        
        //since the head is set inactive later, AddBody is added to the event on Start
        EventManager.DeathEvent += AddBody;

    }

    public void Move() {
        GameObject temp;
        nextPos = head.transform.position;

        //look at the currentdirection and move accordingly
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

        //Move the head to the newly instantiated snake part
        temp = pool.GetObject(nextPos);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        //currentlength is the amount of active snakeobjects, this needs to be equal to the maxlength, since that is increased whenever a player hits the mouse
        if (currentLength >= maxLength) {
            TailFunction();
        }
        else {
            currentLength++;
        }
        return;
    }

    //Set the enum to a new direction based on the pressed WASD key
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
                    spawnPos = nextPos + (goUp * 2);
                    break;

                case Direction.Down:
                    spawnPos = nextPos + (goDown * 2);
                    break;

                case Direction.Left:
                    spawnPos = nextPos + (goLeft * 2);
                    break;

                case Direction.Right:
                    spawnPos = nextPos + (goRight * 2);
                    break;
            }
            //get bullet from objectpool
            bulletPool.GetObject(spawnPos, direction);
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
