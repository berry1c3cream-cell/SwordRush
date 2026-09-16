using UnityEngine;

public class PMovimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float sensibilidad = 2f;
    public float gravedad = -20f;

    public Transform camara;

    private CharacterController jugador;
    private float rotacionX = 0f;
    private float velocidadVertical = 0f;

    void Start()
    {
        jugador = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * horizontal +
                             transform.forward * vertical;

        jugador.Move(movimiento * velocidad * Time.deltaTime);

        // Movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

        transform.Rotate(Vector3.up * mouseX);

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Gravedad
        if (jugador.isGrounded && velocidadVertical < 0)
        {
            velocidadVertical = -2f;
        }

        velocidadVertical += gravedad * Time.deltaTime;

        Vector3 gravedadMovimiento = Vector3.up * velocidadVertical;

        jugador.Move(gravedadMovimiento * Time.deltaTime);
    }
}
