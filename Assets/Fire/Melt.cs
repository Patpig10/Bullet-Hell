using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IceBlock"))
        {
            StartCoroutine(MeltIceBlock(other.gameObject));
        }
    }

    IEnumerator MeltIceBlock(GameObject iceBlock)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            iceBlock.transform.localScale -= new Vector3(2f, 2f, 2f) * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(iceBlock);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
