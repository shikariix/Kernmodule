using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Bullet: 
 * Keeps track of the direction of the bullet.
 * Makes sure the bullets are removed when no longer needed.
 */

public class Bullet : MonoBehaviour {
    public Snake snake;
    public SnakeHead.Direction direction;
    private int speed;

    void Update() {
        switch (direction) {
            case SnakeHead.Direction.Up:
                //move up
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                break;

            case SnakeHead.Direction.Down:
                //move down
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                break;

            case SnakeHead.Direction.Left:
                //move left
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                break;

            case SnakeHead.Direction.Right:
                //move right
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                break;
        }
        if (transform.position.z > 100 || transform.position.x > 100) {
            Destroy(gameObject);
        }
    } 

    public void ChangeDir(SnakeHead.Direction dir) {
        direction = dir;
    }

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.tag == "Mouse") {
            EventManager.StartEvent();
        }
    }

}
