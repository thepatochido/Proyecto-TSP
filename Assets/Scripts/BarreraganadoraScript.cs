using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraganadoraScript : MonoBehaviour
{
    public ControladorColeccionablesScript controladorColeccionables;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && controladorColeccionables.collectedCount == controladorColeccionables.totalCollectibles)
        {
            Debug.Log("¡Ganaste!");
          
        }
    }

    /*private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScene", LoadSceneMode.single);

    }*/
}