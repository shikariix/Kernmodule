using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Mouse:
 * Has a dying function in case it dies. Plays audio when it dies. Generates a random position for itself. That's basically it.
 */

public class Mouse : MonoBehaviour, IDamagable {
    
    private AudioSource audioSource;

    private void Start () {
        audioSource = GetComponent<AudioSource>();
        transform.position = GeneratePos ();
	}

    private void OnEnable() {
		EventManager.DeathEvent += Die;
	}

    private void OnDisable() {
		EventManager.DeathEvent -= Die;
	}

    //The mouse dies when hit with a bullet
    public void Die() {
		//put mouse on new position
		transform.position = GeneratePos ();

        audioSource.Play();
    }

	private Vector3 GeneratePos() {
        //generate a random xPos and zPos value
        //put it in a vector3
        //return the vector into the transform.position of the mouse
        //I should still make it so that the mouse cannot spawn under the snake or off the screen
        int xPos = Random.Range(1, 40);
        int zPos = Random.Range (1, 32);
        Vector3 newPos = new Vector3 (xPos, 0.5f, zPos);
        
		return newPos;
	}
}
