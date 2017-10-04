using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour, IDamagable {



	void Start () {
		//Generate a random position on start
	}

	//The mouse dies when hit with a bullet
	public void Die() {
		//Generate a new position on death
		//Give player a point
	}

	Vector3 GeneratePos() {
		//generate a random xPos and zPos value
		//put it in a vector3
		//return the vector into the transform.position of the mouse
		//It cannot generate on a tile that is taken by the snake

		Vector3 newPos = new Vector3 (0, 0, 0);

		return newPos;
	}
}
