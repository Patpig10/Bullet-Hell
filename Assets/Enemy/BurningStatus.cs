using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningStatus : MonoBehaviour
{
    public int burnDamagePerSecond = 1;
    public float burnDuration = 5.0f;

    private float burnTimer = 0.0f;
    private bool isBurning = false;

    // Add this variable
    private Enemy enemy;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBurning)
        {
            burnTimer += Time.deltaTime;
            if (burnTimer >= 2.0f && burnTimer - Time.deltaTime < 2.0f)
            {
                IsActive();
                enemy.TakeDamage(burnDamagePerSecond);
                burnTimer = 0.0f;
            }
            if (burnTimer >= burnDuration)
            {
                StopBurning();
            }
        }
    }

    public void StartBurning()
    {
        isBurning = true;
        burnTimer = 0.0f;
    }

    public bool IsActive()
    {
        return isBurning;
    }

    public void StopBurning()
    {
        isBurning = false;
    }
}
