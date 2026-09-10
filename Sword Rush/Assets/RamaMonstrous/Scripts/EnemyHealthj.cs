using UnityEngine;

public class EnemyHealthj : MonoBehaviour
{
    public EnemySpawner spawner;
    public bool debug = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        spawner.EnemyDefeated();
    }
}
