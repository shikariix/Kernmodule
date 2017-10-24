using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Snake: 
 * Contains a getter and a setter for the SnakeHead to make the snake with.
 * Also manages death since that is something that can be caused by every body part.
 */

public class Snake : MonoBehaviour, IDamagable {
    
    private Snake next;
    
    //Setter and Getter for snake movement
	public void SetNext(Snake IN) {
		next = IN;
	}
    
    public Snake GetNext() {
        return next;
    }

    public void RemoveTail() {
        gameObject.SetActive(false);
    }

    //Dying redirects to the gameover screen
    public void Die() {
        SceneChanger.GameOver();
    }

    //when the snake hits pretty much anything, it dies
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet" || col.gameObject.tag == "Mouse") {
            Die();
        }
    }

}
