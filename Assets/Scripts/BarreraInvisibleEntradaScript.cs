using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraInvisibleEntradaScript : MonoBehaviour
{
   public AudioSource alarmaAudioSource;
    public float tiempoDeAlarma = 100f;  // Duración del temporizador en segundos
    private float tiempoRestante;

    private bool alarmaActivada = false;

    void Start()
    {
        tiempoRestante = tiempoDeAlarma; // Inicializar el tiempo restante
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alarmaActivada)
        {
            // Activar la alarma
            alarmaAudioSource.Play();

            // Iniciar el temporizador progresivo
            InvokeRepeating("ActualizarTemporizador", 0f, 1f); // Llama a ActualizarTemporizador cada segundo

            alarmaActivada = true;
        }
    }

    private void ActualizarTemporizador()
    {
        tiempoRestante -= 1f; // Decrementar el tiempo restante

        if (tiempoRestante <= 0f)
        {
            // Si el tiempo se agota, detener la alarma y detener la invocación repetida
            alarmaAudioSource.Stop();
            CancelInvoke("ActualizarTemporizador");
        }

        // Aquí puedes realizar acciones adicionales con el tiempo restante, como mostrarlo en la interfaz de usuario.
        Debug.Log("Tiempo restante: " + tiempoRestante);
    }
}