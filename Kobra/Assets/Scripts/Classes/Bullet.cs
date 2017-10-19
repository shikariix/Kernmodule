using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public static Snake snake;
    public Snake.Direction direction;
    private int speed;

    void Start() {
        snake = GameObject.Find("Snake").GetComponent<Snake>();
    }

    void Update() {
        direction = snake.GetDir();
        switch (direction) {
            case Snake.Direction.Up:
                //move up
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                break;

            case Snake.Direction.Down:
                //move down
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                break;

            case Snake.Direction.Left:
                //move left
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                break;

            case Snake.Direction.Right:
                //move right
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                break;
        }
    }
    
}
