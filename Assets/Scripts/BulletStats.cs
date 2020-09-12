using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats : MonoBehaviour {
	
	public float Speed;
	public float Dmg;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * Time.deltaTime * Speed);

    }
	
	void OnTriggerStay2D(Collider2D Trigger){
		Destroy(this.gameObject, 0.0001f);
	}
}
