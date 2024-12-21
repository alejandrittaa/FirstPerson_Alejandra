using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float factorGravedad;
    CharacterController characterController;
    [SerializeField] private float alturaSalto;

    [Header("Deteccion de suelo")]
    [SerializeField] private float radioDeteccion;
    private Vector3 movimientoVertical;
    [SerializeField] private LayerMask queEsSuelo;
    //para obtener unicamente el componente transform del gameobject pies de manera directa
    [SerializeField] private Transform pies;



    void Start()
    {
        //para ocultar el raton, lo bloquea en el centro de la pantalla y lo oculta
        Cursor.lockState = CursorLockMode.Locked;

        //obtenemos una vez el componente character controller, y lo almacenamos en una variable de tipo character controller
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        MoverYRotar();
        AplicarGravedad();
        DetectarSuelo();

        if (DetectarSuelo())
        {
            //cada vez que caigamos al suelo, cancelamos la gravedad
            movimientoVertical.y = 0;
            Saltar();
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //aplicamos formula de salto para saltar la alto de alturasalto
            movimientoVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaSalto);
        }

    }

    void MoverYRotar()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector3(h, v).normalized;

        //creamos esto para que los ojos del jugador cuadren con el movimiento del mismo, ya que si gira la cabeza, tendra que moverse hacia delante y demás, en base a la rotación de su cabeza
        //giramos sobre la Y, que es el palo que atraviesa al jugador por en medio
        float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        //roto el cuerpo a la vez que la cabeza
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        //si la magnitud/tamaño del vector es mayor de 0, si es positiva. 
        if (input.magnitude > 0)
        {

            //mi movimiento queda rotado en base del ángulo calculado (un cuaternion indica una rotacion, mientras que un vector indica una posición)
            //tu frontal pasa a ser el ángulo que tenga la cámara
            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;

            //como no va por físicas, tendremos que multiplicarlo por Time.deltaTime
            characterController.Move(movimiento * velocidadMovimiento * Time.deltaTime);

        }
    }

    private void AplicarGravedad()
    {
        //mi velocidadVertical, va en aumento a cierto factor por segundo.
        //se multiplica 2 veces por delta porque la operacion de la gravedad es m/s2 (al cuadrado)
        movimientoVertical.y += factorGravedad * Time.deltaTime;
        characterController.Move(movimientoVertical * Time.deltaTime);
    }

    private bool DetectarSuelo()
    {
        //crear un esfera de deteccion en los pies con cierto radio.
        bool enSuelo = Physics.CheckSphere(pies.position, radioDeteccion, queEsSuelo);
        return enSuelo;
    }

    //hacemos esto para poder ver la bola dibujada y saber que esta haciendo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //si ponemos DrawWireSphere, saldra solo lineas formando una esfera.
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }

}
