using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IDamagable {

	private int lenght;

	//I need to identify the gameobject that I want to use as body
	//The length decides how many bodyparts there should be
	//The length can be increased and decreased

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//the snake dies when the head hits its body, a bullet hits it, or when it hits the mouse
	public void Die() {
		
	}
}
