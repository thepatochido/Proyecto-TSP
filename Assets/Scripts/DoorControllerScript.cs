using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerScript : MonoBehaviour
{
    public Animator door;

    private void OnTriggerEnter(Collider other)
    {
        door.Play("AOpenAnimation");
    }
    private void OnTriggerExit(Collider other)
    {
        door.Play("ACloseAnimation");
    }
}
