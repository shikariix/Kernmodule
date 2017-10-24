using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour {

    public const int MAX_BULLETS = 15;
    public GameObject bulletPrefab;

    private Bullet[] objects;
    private int currentBullet = 0;

    public SnakeHead.Direction direction;

    void Start() {
        objects = new Bullet[MAX_BULLETS];
        for (int i = 0; i < MAX_BULLETS; ++i) {
            //this creates the objects all on the same position
            //position should be changed on load
            objects[i] = Instantiate(bulletPrefab).GetComponent<Bullet>();
            objects[i].gameObject.SetActive(false);
        }
    }

    //returns bullet in the given position so it can be used
    public GameObject GetObject(Vector3 pos, SnakeHead.Direction dir) {
        //activate current bullet, point it in the right direction
        Bullet bullet = objects[currentBullet];
        bullet.gameObject.transform.position = pos;
        bullet.ChangeDir(dir);
        bullet.gameObject.SetActive(true);

        //cycle through our bullets, make sure to loop around
        if (++currentBullet == MAX_BULLETS) {
            currentBullet = 0;
        }

        return bullet.gameObject;
    }

}
