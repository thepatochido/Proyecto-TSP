using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOverScript : MonoBehaviour
{

    public void Reiniciar(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
    }

    public void MenuPrincipal(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
