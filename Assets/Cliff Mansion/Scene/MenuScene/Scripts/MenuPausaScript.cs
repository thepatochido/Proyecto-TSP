using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaScript : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa=false;
    public GameObject MenuSalir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Pausa==false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa=true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
                for(int i=0; i<sonidos.Length; i++)
                {
                    sonidos[i].Pause();
                }
            }
            else if(Pausa==true)
            {
                Reanudar();
            }
        }
        
    }

    public void Reanudar()
    {
        ObjetoMenuPausa.SetActive(false);
        MenuSalir.SetActive(false);
        Pausa=false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        for(int i=0; i<sonidos.Length; i++)
        {
            sonidos[i].Play();
        }
    }
    public void IrAlMenu(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
