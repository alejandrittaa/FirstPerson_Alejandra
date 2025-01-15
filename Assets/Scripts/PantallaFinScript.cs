using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaFinScript : MonoBehaviour
{

    public GameObject canvasFinal; // Arrastra el CanvasFinal desde el Inspector

    public void MostrarPantallaFinal()
    {
        // Activa el Canvas de Finalización
        canvasFinal.SetActive(true);
    }
    public void ReiniciarNivel()
    {
        // Reinicia la escena actual
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Space_Forest");
    }

    public void SalirJuego()
    {
        // Detener el modo Play en el editor
        EditorApplication.isPlaying = false;
    }
}
