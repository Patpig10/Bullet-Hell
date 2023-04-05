using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Material GrassBurn;


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
    }

    /* if(gameObject.CompareTag("Poison"))
         {
         this.GetCompnent<Rigidbody>().velocity = transform.foward* _poisonSpeed;
         } */

    // Update is called once per frame
    void Update()
    {
        
    }
}
