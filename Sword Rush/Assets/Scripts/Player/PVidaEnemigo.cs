using UnityEngine;

public class PVidaEnemigo : MonoBehaviour
{
    public int vida = 3;

    public void RecibirDaño(int daño)
    {
        vida -= daño;

        Debug.Log("Vida del enemigo: " + vida);

        if (vida <= 0)
        {
            Debug.Log("Enemigo muerto");
            Destroy(gameObject);
        }
    }
}
