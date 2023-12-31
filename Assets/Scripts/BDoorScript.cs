using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDoorScript : MonoBehaviour
{
    public Animator door;

    private void OnTriggerEnter(Collider other)
    {
        door.Play("BOpenAnimation");
    }
    private void OnTriggerExit(Collider other)
    {
        door.Play("BCloseAnimation");
    }
}
