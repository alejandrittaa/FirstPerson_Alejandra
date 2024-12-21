using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    [SerializeField] private float velocidadMovimiento;
    CharacterController characterController;

    void Start()
    {
        //obtenemos el charactercontroller
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoverYRotar();
    }

    void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Vector2 input = new Vector3 (h, v).normalized;

        //para que los ojos del jugador cuadren con el movimiento del mismo
        float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        //para que el cuerpo se oriente hacia donde me muevo
        transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

        //si la magnitud/tamaño del vector es mayor de 0 (si es positiva)...
        if(input.magnitude > 0)
        {
            //mi frontal para a ser el ángulo que tenga la cámara (mi movimeinto queda rotado en base al ángulo calculado)
            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;

            //como no va por físicas, tendremos que multiplicarlo por Time.deltaTime
            characterController.Move(movimiento * velocidadMovimiento * Time.deltaTime);
        }
    }

}
