using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAudioScript : MonoBehaviour
{
    public AudioClip musica1;  // M�sica que arranca por default
    public AudioClip musica2;  // Nueva m�sica despu�s de recoger todos los coleccionables

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Referencia del audioSource, para no llamarlo muchas veces
        audioSource.clip = musica1; //Le guardo la primera musica
        audioSource.Play(); //Toco la musica
    }

    
      public void ChangeMusic() //Cambia la musica, la razon es despues
    {
        audioSource.clip = musica2;
        audioSource.Play();
    }
}
