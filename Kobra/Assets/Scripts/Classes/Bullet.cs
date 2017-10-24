using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Bullet: 
 * Keeps track of the direction of the bullet.
 * Makes sure the bullets are removed when no longer needed.
 */

public class Bullet : MonoBehaviour {
    public Snake snake;
    private SnakeHead.Direction direction;
    private int speed;

    private Vector3 moveUp = new Vector3(0, 0, 1);
    private Vector3 moveDown = new Vector3(0, 0, -1);
    private Vector3 moveLeft = new Vector3(-1, 0, 0);
    private Vector3 moveRight = new Vector3(1, 0, 0);

    AudioSource audioSource;

    void Update() {
        //set direction of bullet based on direction of snake head
        switch (direction) {
            case SnakeHead.Direction.Up:
                transform.position += moveUp;
                break;

            case SnakeHead.Direction.Down:
                transform.position += moveDown;
                break;

            case SnakeHead.Direction.Left:
                transform.position += moveLeft;
                break;

            case SnakeHead.Direction.Right:
                transform.position += moveRight;
                break;
        }
        if (transform.position.z > 100 || transform.position.x > 100) {
            gameObject.SetActive(false);
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


    void OnEnable() {
        EventManager.DeathEvent += Disable;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void OnDisable() {
        EventManager.DeathEvent -= Disable;
    }

    void Disable() {
        gameObject.SetActive(false);
    }
}
