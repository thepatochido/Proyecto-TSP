using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarreraganadoraScript : MonoBehaviour
{
    public ControladorColeccionablesScript controladorColeccionables;

    public GameObject blackImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && controladorColeccionables.collectedCount == controladorColeccionables.totalCollectibles)
        {
            Debug.Log("¡Ganaste!");
            blackImage.SetActive(true);
            StartCoroutine(DelaySceneChange());
          
        }
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
    }
}