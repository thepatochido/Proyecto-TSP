using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuWinScript : MonoBehaviour
{

    

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    public void Reiniciar(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena,LoadSceneMode.Single);
    }

    public void MenuPrincipal(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena,LoadSceneMode.Single);
    }

    public void Salir()
    {
        Application.Quit();
    }

    void Update()
    {
        
    }
}
