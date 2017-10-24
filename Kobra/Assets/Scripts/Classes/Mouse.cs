using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour, IDamagable {

	Renderer render;
    AudioSource audioSource;

	void Start () {
        render = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        transform.position = GeneratePos ();
	}

	void OnEnable() {
		EventManager.DeathEvent += Die;
	}

	void OnDisable() {
		EventManager.DeathEvent -= Die;
	}

	//The mouse dies when hit with a bullet
	public void Die() {
		//put mouse on new position
		transform.position = GeneratePos ();

        //make sure the mouse cannot be offscreen
        //this appears to be broken honestly
        while (!render.isVisible) {
            transform.position = GeneratePos();
        }

        audioSource.Play();
    }

	Vector3 GeneratePos() {
        //generate a random xPos and zPos value
        //put it in a vector3
        //return the vector into the transform.position of the mouse
        //I should still make it so that the mouse cannot spawn under the snake
        int xPos = Random.Range(1, 48);
        int zPos = Random.Range (1, 32);
        Vector3 newPos = new Vector3 (xPos, 0.5f, zPos);
        
		return newPos;
	}
}
