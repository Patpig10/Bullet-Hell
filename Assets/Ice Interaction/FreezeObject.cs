using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeObject : MonoBehaviour
{
    private string originalTag;
    private bool isFrozen;

    public float freezeTime = 2f; // How long to freeze the object (in seconds)

    void Start()
    {
        originalTag = gameObject.tag;
        isFrozen = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice") && !isFrozen)
        {
            StartCoroutine(FreezeObjectForSeconds());
        }
    }

    IEnumerator FreezeObjectForSeconds()
    {
        gameObject.tag = "Frozen";
        isFrozen = true;

        yield return new WaitForSeconds(freezeTime);

        gameObject.tag = originalTag;
        isFrozen = false;
    }
}