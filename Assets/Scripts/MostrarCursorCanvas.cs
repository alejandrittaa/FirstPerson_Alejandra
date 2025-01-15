using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarCursorCanvas : MonoBehaviour
{
    private void OnEnable()
    {
        // Mostrar el cursor cuando el Canvas esté activo
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
    }

    private void OnDisable()
    {
        // Ocultar el cursor cuando el Canvas se desactive
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
    }
}
