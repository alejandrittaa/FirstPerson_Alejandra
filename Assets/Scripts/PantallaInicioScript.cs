using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicioScript : MonoBehaviour
{
    public GameObject canvasInicio;

    public void IniciarJuego()
    {
        Debug.Log("Bot�n Play presionado. Iniciando el juego...");

        // Desactiva el Canvas de Inicio
        canvasInicio.SetActive(false);

        // Carga la escena principal
        SceneManager.LoadScene("Space_Forest"); 
    }
}
