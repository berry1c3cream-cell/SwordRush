using UnityEngine;

public class PVidaPlayer : MonoBehaviour
{
    public int vida = 100;

    private bool muerto = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !muerto)
        {
            RecibirDaño(1);
        }
    }

    public void RecibirDaño(int daño)
    {
        if (muerto)
            return;

        vida -= daño;

        Debug.Log("Vida del jugador: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        muerto = true;

        PMovimiento movimiento = GetComponent<PMovimiento>();

        if (movimiento != null)
        {
            movimiento.enabled = false;
        }

        Debug.Log("El jugador ha muerto");
    }
}
