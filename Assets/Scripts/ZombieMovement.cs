using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{


    Transform targetAggro;

    public AudioSource zombAudio;
    float nextAudio;

    // Start is called before the first frame update
    void Start()
    {
        nextAudio += Random.Range(10f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextAudio)
        {
            zombAudio.Play();
            nextAudio += Random.Range(10f, 30f);
        }
    }

    void OnTriggerEnter(Collider coll){
        if(coll.tag == "player" && coll.name == "agro"){
            targetAggro = coll.transform.parent;
        }
    }
}
