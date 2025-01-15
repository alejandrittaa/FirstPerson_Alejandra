using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicioScript : MonoBehaviour
{
    public void IniciarJuego()
    {
        // Carga la escena principal del juego
        SceneManager.LoadScene("Space_Forest");
    }
}
