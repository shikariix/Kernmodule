using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour, IDamagable {


	void Start () {
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
		//Give player a point
	}

	Vector3 GeneratePos() {
		//generate a random xPos and zPos value
		//put it in a vector3
		//return the vector into the transform.position of the mouse
		//It cannot generate on a tile that is taken by the snake
		int xPos = Random.Range(1, 32);
		int zPos = Random.Range (1, 32);

		Vector3 newPos = new Vector3 (xPos, 0.5f, zPos);

		return newPos;
	}
}
