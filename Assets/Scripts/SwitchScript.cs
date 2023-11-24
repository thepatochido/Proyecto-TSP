using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject lightObject;
    public bool lights;
    private bool lightOnOff;

    public void OnOffLight()
    {
        lightOnOff = !lightOnOff;
        if (lightOnOff == true)
        {
            lightObject.SetActive(true);
        }
        if (lightOnOff == false)
        {
            lightObject.SetActive(false);
        }
    }
}
