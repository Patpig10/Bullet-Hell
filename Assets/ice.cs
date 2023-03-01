using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ice : MonoBehaviour
{
    public Animator openAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            openAnimator.SetTrigger("ice");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
