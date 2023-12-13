using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeccionablesScript : MonoBehaviour
{
   
    public float interactionRadius = 0.5f;  // Radio de interacci�n para hacer clic del jugador
    public AudioClip collectSound;  // Sonido al recoger el coleccionable

    private bool isCollected = false; //Inicia falso, se hace verdadero para desaparecerlo

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0,Time.time*100f,0); //Gira cada ciero tiempo en el eje y, para crear el movimiento

        // Verifica si el jugador hace clic derecho en el coleccionable y est� dentro del radio de interacci�n
        if (Input.GetMouseButtonDown(0) && IsPlayerNearby())
        {
            Collect();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        // Verifica si el jugador entra en contacto con el coleccionable
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (!isCollected)
        {
            

            // Reproduce el sonido de recoger
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Desactiva el objeto para que no se pueda recoger nuevamente
            gameObject.SetActive(false);

            isCollected = true; //Hace verdadero para dejar de usar el objeto despu�s de destruirlo
        }
    }

    bool IsPlayerNearby()
    {
        // Verifica si el jugador est� dentro del radio de interacci�n
        Vector3 playerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition.z = transform.position.z; // Ajusta la posici�n Z para que sea la misma que la del coleccionable

        return Vector3.Distance(playerPosition, transform.position) <= interactionRadius;
    }
}
