using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    public int range;
    
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.collider.GetComponent<SwitchScript>()==true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hit.collider.GetComponent<SwitchScript>().lights == true)
                    {
                        hit.collider.GetComponent<SwitchScript>().OnOffLight();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
}
