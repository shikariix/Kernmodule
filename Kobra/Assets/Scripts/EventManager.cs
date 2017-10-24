using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	
    //this event contains all functions that need to happen when the player shoots a mouse.
	public delegate void OnMouseDeath();
	public static event OnMouseDeath DeathEvent;

    public static void StartEvent() {
        if (DeathEvent != null) {
            DeathEvent();
        }
	}

}
