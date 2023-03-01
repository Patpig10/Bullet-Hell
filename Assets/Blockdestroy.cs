using UnityEngine;

public class Blockdestroy : MonoBehaviour
{
    public float delay = 5f; // The delay in seconds before the object is destroyed

    void Start()
    {
        Destroy(gameObject, delay); // Destroy the object after the specified delay
    }
}
