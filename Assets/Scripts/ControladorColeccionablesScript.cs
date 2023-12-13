using UnityEngine;
using System.Collections.Generic;

public class ControladorColeccionablesScript : MonoBehaviour
{
    public int totalCollectibles = 14;
    private int collectedCount = 0;
    private List<ColeccionablesScript> coleccionablesRecogidos = new List<ColeccionablesScript>();

    public ControladorAudioScript audioManager;
    public ColeccionablesScript[] coleccionables;

    void Update()
    {
        foreach (var coleccionable in coleccionables)
        {
            if (coleccionable != null && coleccionable.IsCollected() && !coleccionablesRecogidos.Contains(coleccionable))
            {
                Debug.Log("Coleccionable recogido: " + coleccionable.gameObject.name);
                coleccionablesRecogidos.Add(coleccionable);
                IncrementarContador();
            }
        }
    }

    void IncrementarContador()
    {
        collectedCount++;
        Debug.Log("Contador incrementado. Nuevo valor: " + collectedCount);

        if (collectedCount == totalCollectibles)
        {
            audioManager.ChangeMusic();
            Debug.Log("Todos los coleccionables recogidos.");
        }
    }
}