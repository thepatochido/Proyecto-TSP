using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ControladorColeccionablesScript : MonoBehaviour
{
    public int totalCollectibles = 14;
    public int collectedCount = 0;

    public TextMeshProUGUI itemText;

    private List<ColeccionablesScript> coleccionablesRecogidos = new List<ColeccionablesScript>();
    public ControladorAudioScript audioManager;
    public ColeccionablesScript[] coleccionables;

    private void Start()
    {
        itemText.text = "00";
    }

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
        UpdateItemText();
        Debug.Log("Contador incrementado. Nuevo valor: " + collectedCount);

        if (collectedCount == totalCollectibles)
        {
            audioManager.ChangeMusic();
            Debug.Log("Todos los coleccionables recogidos.");
        }
    }

    void UpdateItemText()
    {
        if (collectedCount < 10)
        {
            itemText.text = "0" + collectedCount.ToString();
        }else if(collectedCount >= 10)
        {
            itemText.text = collectedCount.ToString();

        }
    }
}