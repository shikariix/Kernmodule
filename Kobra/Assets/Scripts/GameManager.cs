using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Snake player;
	public Mouse mouse;
    public GameObject bulletPrefab;

    private int currentLength;
	public int maxLength;

	void Start() {
        player = FindObjectOfType<Snake>();
        mouse = FindObjectOfType<Mouse> ();
		//pool = GetComponent<ObjectPool> ();

		maxLength = 4;
		currentLength = 1;

		InvokeRepeating ("TimerInvoke", 0, 0.1f);
	}

	void Update() {
		if (Input.anyKey) {
			player.ChangeDir ();
            player.Shoot();
		}

		if (player.head.transform.position == mouse.transform.position) {
			EventManager.StartEvent ();
		}
	}

	void TimerInvoke() {
		player.Move ();

		if (currentLength >= maxLength) {
			player.TailFunction ();
		} else {
			currentLength++;
		}
	}


    void OnEnable() {
        EventManager.DeathEvent += AddBody;
    }

    void OnDisable() {
        EventManager.DeathEvent -= AddBody;
    }

    public void AddBody() {
        maxLength++;
    }

}
