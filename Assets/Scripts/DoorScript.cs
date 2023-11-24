using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float speed;
    public float angle;
    public Vector3 direction;

    public bool puedeAbrir;
    public bool abrir;

    void Start()
    {
        angle = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Round(transform.eulerAngles.z) != angle)
        {
            transform.Rotate(direction * speed);
        }
        if (Input.GetButtonDown("Fire1") && puedeAbrir == true && abrir == false)
        {
            angle = 270;
            direction = Vector3.up;
            abrir = true;
        }
        if (Input.GetButtonDown("Fire1") && puedeAbrir == true && abrir == true)
        {
            angle = 180;
            direction = Vector3.down;
            abrir = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            puedeAbrir = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            puedeAbrir = false;
        }
    }
}
