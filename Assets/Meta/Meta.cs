using UnityEngine;

public class Meta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entró a la meta: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡GANASTE!");
        }
    }
}