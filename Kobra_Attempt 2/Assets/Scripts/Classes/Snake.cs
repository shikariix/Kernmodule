using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IDamagable {

    private float speed;

    public GameObject snakeBody;

    private Snake next;

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

}
