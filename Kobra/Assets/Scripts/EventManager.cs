using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	

	public delegate void OnMouseDeath();
	public static event OnMouseDeath DeathEvent;

    public static void StartEvent() {
        if (DeathEvent != null) {
            DeathEvent();
        }
	}

}
