using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	float TimerAttack;
	public float TimeBeforeNextAtt = 0.3f;
	public GameObject Bullet;
	public float Dmg = 10;
	public float Speed = 0.3f;
	public float TimeBeforeDestroy = 5f;
	public string Tire;
	
	// Use this for initialization
	void Start () {
		//Tire = this.transform.parent.GetComponent<PlayerMovement>().Tire;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(Tire) && TimerAttack <= Time.time - TimeBeforeNextAtt)
        {
            GameObject balle = Instantiate(Bullet, transform.position, transform.rotation);
            Destroy(balle, TimeBeforeDestroy);
            balle.GetComponent<BulletStats>().Dmg = Dmg;
            balle.GetComponent<BulletStats>().Speed = Speed;
            TimerAttack = Time.time + TimeBeforeNextAtt;
        }
    }
}
