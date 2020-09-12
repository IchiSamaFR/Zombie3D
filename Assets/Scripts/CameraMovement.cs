using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public bool inventoryIsOpen = false;

	public GameObject Parent;
    public int SmoothSpeed = 20;


    public float camSpeedX = 1;
    float xRotation = 0;

    public float camSpeedY = 1;
    float yRotation = 0;

    float angleMax = 45;


    // Use this for initialization
    void Start () {
        xRotation = Parent.transform.localRotation.y * 116;
        yRotation = transform.localRotation.x * 116;
    }

    // Update is called once per frame
    void Update ()
    {
        raycast();

        if (inventoryIsOpen)
        {
            Parent.transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
            return;

        }
        
        float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime * camSpeedX;
        xRotation += mouseX;

        

        float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime * camSpeedY;

        if(yRotation < angleMax && yRotation > -angleMax)
        {
            yRotation -= mouseY;

            if(yRotation < -angleMax)
            {
                yRotation = -angleMax;
            }
            if (yRotation > angleMax)
            {
                yRotation = angleMax;
            }
        }

        if (yRotation >= angleMax && mouseY > 0)
        {
            yRotation--;
        } else if (yRotation <= -angleMax && mouseY > 0)
        {
            yRotation++;
        }

        if (Parent.GetComponent<PlayerMovement>().Direction == 1)
        {
            Parent.transform.localRotation = Quaternion.Euler(0f, xRotation+90, 0f);
            this.transform.localRotation = Quaternion.Euler(yRotation, -90, 0f);
        }
        else if (Parent.GetComponent<PlayerMovement>().Direction == -1)
        {
            Parent.transform.localRotation = Quaternion.Euler(0f, xRotation-90, 0f);
            this.transform.localRotation = Quaternion.Euler(yRotation, 90, 0f);
        }
        else
        {
            Parent.transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
            this.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        }

        //Suivre en différer
        /*
        float difY = target.transform.position.y - this.transform.position.y + 20;
        float difX = (target.transform.position.x - this.transform.position.x);
        float difZ = (target.transform.position.z - this.transform.position.z) - ((this.transform.position.y - target.transform.position.y) * 0.35f);

        if (difX > 0.01f || difX < -0.01f)
        {
            if (difZ > 0.01f || difZ < -0.01f)
            {
                this.transform.position += new Vector3(difX / SmoothSpeed, difY, difZ / SmoothSpeed);
            }
            else
            {
                this.transform.position += new Vector3(difX / SmoothSpeed, difY, 0);
            }
        } else
        {
            if (difZ > 0.01f || difZ < -0.01f)
            {
                this.transform.position += new Vector3(0, difY, difZ / SmoothSpeed);
            }
        }*/

    }


    [SerializeField] private List<string> selectable = new List<string>(new string[] { "Selectable", "Chest" });
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private GameObject saved;

    void raycast()
    {
        if (saved)
        {
            if(saved.tag == "Chest")
            {
                saved.GetComponent<BoxInventory>().canOpen = false;
            }
            saved.GetComponent<Renderer>().material = defaultMaterial;
            saved = null;
        }


        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 7))
        {
            var select = hit.transform;

            if (selectable.IndexOf(select.tag) >= 0)
            {
                defaultMaterial = select.GetComponent<Renderer>().material;
                select.GetComponent<Renderer>().material = highlightMaterial;

                saved = select.gameObject;

                if (select.tag == "Chest")
                {
                    saved.GetComponent<BoxInventory>().canOpen = true;
                    saved.GetComponent<BoxInventory>().Inventory = Parent.GetComponent<PlayerInventory>();
                }
            }
        }
    }
}
