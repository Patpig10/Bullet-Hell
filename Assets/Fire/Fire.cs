using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Material GrassBurn;
    public GameObject fireParticlePrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grass"))
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            Material[] materials = renderer.materials;
            int greenMaterialIndex = -1;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name.Contains("Grass"))
                {
                    greenMaterialIndex = i;
                    break;
                }
            }
            if (greenMaterialIndex >= 0)
            {
                materials[greenMaterialIndex] = GrassBurn;
                renderer.materials = materials;
            }
            Destroy(other.gameObject, 3.0f);
            Debug.Log("Fire collided with Grass");
        }

        // Add particle system when fire bullet hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            BurningStatus burningStatus = other.gameObject.GetComponent<BurningStatus>();
            if (burningStatus != null)
            {
                burningStatus.StartBurning();

                // Spawn particle system
                GameObject particleSystem = Instantiate(fireParticlePrefab, other.gameObject.transform.position, Quaternion.identity);
                Destroy(particleSystem, 5f);
            }
        }
    }
}
