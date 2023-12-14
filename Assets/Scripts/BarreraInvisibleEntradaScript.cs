using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarreraInvisibleEntradaScript : MonoBehaviour
{
    public AudioSource alarmaAudioSource;
    public AudioClip sonidoAlTocar;
    public float tiempoDeAlarma = 90f;
    public float tiempoRestante;
    public float rangoDeApagado = 5f; // Nuevo rango para apagar la alarma
    private bool hasItTurnedOn = false; //Es para evitar multiples iteraciones

    private bool alarmaActivada = false;

    public GameObject blackImage;

    void Start()
    {
        tiempoRestante = tiempoDeAlarma;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alarmaActivada && !hasItTurnedOn)
        {
            alarmaAudioSource.Play();
            InvokeRepeating("ActualizarTemporizador", 0f, 1f);
            alarmaActivada = true;
            hasItTurnedOn = true;
        }
    }

    private void ActualizarTemporizador()
    {
        tiempoRestante -= 1f;

        if (tiempoRestante <= 0f)
        {
            alarmaAudioSource.Stop();
            CancelInvoke("ActualizarTemporizador");
            alarmaActivada = false; // Desactiva la alarma cuando el tiempo ha llegado a cero
        }

        Debug.Log("Tiempo restante: " + tiempoRestante);
        if (tiempoRestante <= 1)
        {
            blackImage.SetActive(true);
            StartCoroutine(DelaySceneChange());
        }
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }

    void Update()
    {
        // Verifica si se presiona la tecla "R" y el jugador est  cerca de la alarma
        if (Input.GetKeyDown(KeyCode.R) && alarmaActivada && EsJugadorCerca())
        {
            ApagarAlarma();
        }

    }

    bool EsJugadorCerca()
    {
        // Verifica si el jugador est  dentro del rango de apagado
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 alarmaPosition = GameObject.FindGameObjectWithTag("AlarmaObjeto").transform.position;
        playerPosition.y = alarmaPosition.y; // Ajusta la posici n Y para que sea la misma que la del objeto


        float distancia = Vector3.Distance(playerPosition, alarmaPosition);
        Debug.Log("Distancia entre jugador y alarma: " + distancia);

        bool estaCerca = distancia <= rangoDeApagado;
        Debug.Log(" El jugador est  cerca? " + estaCerca);

        return estaCerca;
    }

    void ApagarAlarma()
    {
        alarmaAudioSource.Stop();
        tiempoRestante += 30f;
        alarmaActivada = false; // Desactiva la alarma despu s de apagarla

        // Reproducir el sonido al tocar
        if (sonidoAlTocar != null)
        {
            AudioSource.PlayClipAtPoint(sonidoAlTocar, transform.position);
        }

        Debug.Log("Alarma apagada. Tiempo restante: " + tiempoRestante);
    }
}