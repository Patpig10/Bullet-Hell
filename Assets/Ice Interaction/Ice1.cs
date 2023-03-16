using UnityEngine;

public class Ice1 : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn when the ice hits water

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water") && gameObject.CompareTag("Ice"))
        {
            Destroy(gameObject); // Destroy the ice
            Instantiate(prefab, transform.position, transform.rotation); // Spawn the prefab
        }
    }
}
