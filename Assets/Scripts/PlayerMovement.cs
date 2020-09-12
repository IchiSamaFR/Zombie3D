using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject Camera;

    [Header("Anim")]
    public Animator anim;
    public bool isWalking;
    public bool isRunning;
    public bool isWalkBack;
    public int Direction;



    Rigidbody rigid;

    [Header("Stats")]
	public float Speed = 1;
    float FloatSpeed;


    [Header("Key Binding")]
	public string KeyDevant;
	public string KeyArriere;
	public string KeyGauche;
	public string KeyDroite;

	Vector3 Base;
    Dictionary<string, float> speedBoost =
        new Dictionary<string, float>();

	// Use this for initialization
	void Start () {
        rigid = this.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        rigid.velocity = new Vector3(0, -10, 0);


        // || Input.GetKey(KeyDroite) || Input.GetKey(KeyGauche)) && !Input.GetKey(KeyArriere)
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyDevant)){
            speedBoost.Remove("isSprinting");
			speedBoost.Add("isSprinting", 2.5f);
            isRunning = true;

        } else {
            speedBoost.Remove("isSprinting");
            isRunning = false;
        }

        setSpeed();




        if (Input.GetKey(KeyDevant)){

            this.transform.Translate(Vector3.forward * Time.deltaTime * FloatSpeed);
            speedBoost.Remove("isStrafe");
            speedBoost.Remove("isBacking");

            isWalkBack = false;
            isWalking = true;
            Direction = 0;

        }
        else if (Input.GetKey(KeyDroite))
        {
            speedBoost.Remove("isStrafe");
            speedBoost.Remove("isBacking");
            speedBoost.Add("isStrafe", -0.5f);
            this.transform.Translate(Vector3.forward * Time.deltaTime * FloatSpeed);

            isWalkBack = false;
            isWalking = true;
            Direction = 1;

        }
        else if (Input.GetKey(KeyGauche))
        {
            speedBoost.Remove("isStrafe");
            speedBoost.Remove("isBacking");
            speedBoost.Add("isStrafe", -0.5f);
            this.transform.Translate(Vector3.forward * Time.deltaTime * FloatSpeed);

            isWalkBack = false;
            isWalking = true;
            Direction = -1;

        }else if (Input.GetKey(KeyArriere))
        {
            speedBoost.Remove("isStrafe");
            speedBoost.Remove("isBacking");
            speedBoost.Add("isBacking", -0.4f);
            this.transform.Translate(Vector3.back * Time.deltaTime * FloatSpeed);


            isWalkBack = true;
            isWalking = false;
            Direction = 0;
        }
        else
        {
            speedBoost.Remove("isStrafe");
            speedBoost.Remove("isBacking");


            isWalkBack = false;
            isWalking = false;
            Direction = 0;
        }


        loadAnimation();
        

    }

    public void setSpeed(){
        Dictionary<string, float>.ValueCollection values = speedBoost.Values; 
        FloatSpeed = Speed * 5;
        foreach(float val in values){
            FloatSpeed += val * 5;
        }
    }


    public void loadAnimation()
    {
        anim.SetInteger("Direction", Direction);

        if (isRunning)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }
        else if (isWalking)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }
        else {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }

        if (isWalkBack)
        {
            anim.SetBool("isWalkBack", true);
        } else
        {
            anim.SetBool("isWalkBack", false);
        }
    }
}
