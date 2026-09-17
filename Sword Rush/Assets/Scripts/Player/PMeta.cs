using UnityEngine;

public class PMeta : MonoBehaviour
{
    public victory pantalla;
    public PMovimiento movimiento;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entró a la meta: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {

            if (movimiento != null)
            {
                movimiento.enabled = false;
            }

            Debug.Log("¡GANASTE!");
            pantalla.MostrarPantallaWIN();
        }
    }
}
