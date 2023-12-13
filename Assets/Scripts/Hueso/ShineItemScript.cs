using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineItemScript : MonoBehaviour
{
    public float intensidadInicial = 0f;        // Intensidad de luz inicial
    public float intensidadMaxima = 15f;         // Valor máximo de intensidad
    public float duracionAumento = 10f;          // Duración del aumento de intensidad en segundos
    public float duracionDisminucion = 10f;      // Duración de la disminución de intensidad en segundos

    private Light luzObjeto;
    private float tiempo;

    private void Start()
    {
        // Asegúrate de que el objeto tiene un componente de luz
        luzObjeto = GetComponent<Light>();

        // Configura la intensidad inicial de la luz
        luzObjeto.intensity = intensidadInicial;
    }

    private void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo < duracionAumento)
        {
            // Aumenta la intensidad linealmente durante el tiempo de aumento
            float factorAumento = tiempo / duracionAumento;
            luzObjeto.intensity = Mathf.Lerp(intensidadInicial, intensidadMaxima, factorAumento);
        }
        else if (tiempo < duracionAumento + duracionDisminucion)
        {
            // Disminuye la intensidad linealmente durante el tiempo de disminución
            float factorDisminucion = (tiempo - duracionAumento) / duracionDisminucion;
            luzObjeto.intensity = Mathf.Lerp(intensidadMaxima, intensidadInicial, factorDisminucion);
        }
        else
        {
            // Reinicia el tiempo para repetir el ciclo
            tiempo = 0f;
        }
    }
}
