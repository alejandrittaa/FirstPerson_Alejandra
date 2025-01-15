using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaFinScript : MonoBehaviour
{
    public void ReiniciarNivel()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirJuego()
    {
        // Detener el modo Play en el editor
        EditorApplication.isPlaying = false;
    }
}
